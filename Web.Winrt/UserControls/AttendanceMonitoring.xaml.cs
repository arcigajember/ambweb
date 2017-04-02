using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MiFare;
using MiFare.Classic;
using MiFare.Devices;
using MiFare.PcSc;
using Web.DataLayer.Repositories;
using Web.DataLayer.Util;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.Winrt.UserControls
{
    /// <summary>
    /// Interaction logic for AttendanceMonitoring.xaml
    /// </summary>
    public partial class AttendanceMonitoring : UserControl
    {
        public bool IsSchedule { get; set; }
        private readonly StudentRepository _studentRepo;
        private readonly MessageRepository _messageRepo;
        private readonly AttendanceSectionRepository _attendanceSectionRepo;
        private SmartCardReader _reader;
        private MiFareCard _card;

        public AttendanceMonitoring()
        {
            InitializeComponent();
            _attendanceSectionRepo = new AttendanceSectionRepository();
            _studentRepo = new StudentRepository();
            _messageRepo = new MessageRepository();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await GetDevices();
            await LoadListView(await _attendanceSectionRepo.AttendanceToday());
        }
        private async Task GetDevices()
        {
            try
            {
                _reader = await CardReader.FindAsync();
                if (_reader == null)
                {
                    LblLog.Content = "No device reader found";
                }
                else
                {
                    LblLog.Content = "Device ready";
                    _reader.CardAdded += _reader_CardAdded;
                    _reader.CardRemoved += _reader_CardRemoved;
                }
            }
            catch (Exception ex)
            {
                LblLog.Content = "Exception: " + ex.Message;

            }
        }

        private void _reader_CardRemoved(object sender, CardRemovedEventArgs e)
        {
            _card?.Dispose();
            _card = null;
        }

        private void _reader_CardAdded(object sender, CardAddedEventArgs e)
        {
            if (IsSchedule)
            {
                Dispatcher.Invoke(async () =>
                {
                    try
                    {
                        await HandleCard(e);
                    }
                    catch (Exception ex)
                    {
                        LblLog.Content = "CardAdded Exception: " + ex.Message;
                    }
                });
            }
        }

        private async Task HandleCard(CardEventArgs args)
        {
            try
            {
                _card?.Dispose();
                _card = args.SmartCard.CreateMiFareCard();
                var localCard = _card;

                var cardIdentification = await localCard.GetCardInfo();

                LblLog.Content =
                    $"Connected to card\r\nPC/SC device class: {cardIdentification.PcscDeviceClass}\r\nCard name: {cardIdentification.PcscCardName}\r\n";

                if (cardIdentification.PcscDeviceClass == DeviceClass.StorageClass &&
                    (cardIdentification.PcscCardName == CardName.MifareStandard1K ||
                     cardIdentification.PcscCardName == CardName.MifareStandard4K))
                {
                    LblLog.Content = "MIFARE Standard/Classic card detected";

                    var uid = BitConverter.ToString(await localCard.GetUid());

                    Student student = await _studentRepo.StudentSelectByUid(uid);

                    if (student != null)
                    {
                        int timeTypeId = await _messageRepo.StudentCheckTime(student.StudentId);

                        string[] timeType = { "Invalid", "Time In", "Time Out" };

                        if (timeTypeId == 0)
                        {
                            LblLog.Content = $"{timeType[timeTypeId]}: You can come back tomorrow";
                        }
                        else
                        {
                            int attendanceHeaderId = await _messageRepo.AttendanceHeaderInsert(student.StudentId);

                            var attendanceDetails =
                                await
                                    _messageRepo.SelectAttandanceDetailsId(
                                        await
                                            _messageRepo.AttendanceDetailsInsert(attendanceHeaderId, timeTypeId));


                            string response = "No contacts found";

                            IEnumerable<GuardianContact> result =
                                await _studentRepo.StudentGuardianContact(student.StudentId);

                            var guardianContacts = result as IList<GuardianContact> ?? result.ToList();
                            if (guardianContacts.IsAny())
                            {
                                guardianContacts.ToList().ForEach(async s =>
                                {
                                    string message =
                                        $"Attendance monitoring\r\nStudent: {student.FullName}\r\nSection: {student.Section.SectionName}\r\n{timeType[timeTypeId]}: {attendanceDetails.Time.ToString("h:mm:ss tt")} {attendanceDetails.Date.ToString("d")}";
                                    response = await _messageRepo.SendMessage(s.ContactNumber, message);

                                    await
                                        _messageRepo.AttendanceLogInsert(attendanceHeaderId, s.StudentGuardianId,
                                            response);
                                });
                            }
                            else
                            {
                                await _messageRepo.AttendanceLogInsert(attendanceHeaderId, null, response);
                            }

                        }
                    }
                    else
                    {
                        LblLog.Content = "Cannot find this card";
                    }

                }
                await LoadListView(await _attendanceSectionRepo.AttendanceToday());
            }
            catch (Exception ex)
            {
                LblLog.Content = "HandleCard Exception: " + ex.Message;
            }
        }

        private async Task LoadListView(IEnumerable<Student> model)
        {
            //var studentList = model.Distinct(new ComparerStudent());

            var studentList = model.Distinct(new ComparerStudent()).ToList();

            foreach (var s in studentList)
            {
                s.AttendanceDetails = await _attendanceSectionRepo.AttendanceDetails(s.StudentId);
            }

            AttendanceListView.Items.Clear();
            foreach (var student in studentList)
            {
                if (student.AttendanceDetails.IsAny())
                {
                    var itemView = new AttendanceModelView
                    {
                        FullName = student.FullName,
                        Section = student.Section.SectionName,
                        Address = student.Address
                    };
                    student.AttendanceDetails.ToList().ForEach(s =>
                    {
                        itemView.Date = itemView.Date ?? s.Date.ToString("d");
                        itemView.TimeIn = itemView.TimeIn ?? (s.TimeTypeId == 1 ? s.Time.ToShortTimeString() : null);
                        itemView.TimeOut = itemView.TimeOut ?? (s.TimeTypeId == 2 ? s.Time.ToShortTimeString() : null);

                    });
                    AttendanceListView.Items.Add(itemView);
                }
            }

        }
    }
}
