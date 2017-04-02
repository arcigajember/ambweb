using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MiFare;
using MiFare.Classic;
using MiFare.Devices;
using MiFare.PcSc;
using Web.ClientWpf.Form;
using Web.DataLayer.Repositories;
using Web.DataLayer.Util;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.ClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudentRepository _studentRepo;
        private readonly MessageRepository _messageRepo;
        private SmartCardReader _reader;
        private MiFareCard _card;

        public MainWindow()
        {
            InitializeComponent();
            _studentRepo = new StudentRepository();
            _messageRepo = new MessageRepository();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await GetDevices();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            StudentForm studentForm = new StudentForm();
            this.Hide();
            studentForm.Show();
           
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
            Dispatcher.Invoke(async () =>
            {
                await HandleCard(e);
            });
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
                        IEnumerable<GuardianContact> result =
                            await _studentRepo.StudentGuardianContact(student.StudentId);

                        var guardianContacts = result as IList<GuardianContact> ?? result.ToList();
                        if (guardianContacts.IsAny())
                        {
                            guardianContacts.ToList().ForEach(async s =>
                            {
                                string message =
                                    $"Attendance monitoring\r\nStudent: {student.FullName}\r\nSection: {student.Section.SectionName}\r\nTime: {DateTime.Now}";
                                string response = await _messageRepo.SendMessage(s.ContactNumber, message);
                            });
                        }
                        else
                        {
                            LblLog.Content = "No contact found";
                        }
                    }
                    else
                    {
                        LblLog.Content = "Cannot find this card";
                    }

                }
            }
            catch (Exception ex)
            {
                LblLog.Content = "HandleCard Exception: " + ex.Message;
            }
        }
    }
}
