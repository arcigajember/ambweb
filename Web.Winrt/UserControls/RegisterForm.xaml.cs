using System;
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
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : UserControl
    {
        public bool IsRegister { get; set; }
        private readonly StudentRepository _studentRepo;
        private SmartCardReader _reader;
        private MiFareCard _card;
        private RegisterStudentView _result;

        public RegisterForm()
        {
            InitializeComponent();
            _studentRepo = new StudentRepository();

            Loaded += RegisterForm_Loaded;
        }

        private async void RegisterForm_Loaded(object sender, RoutedEventArgs e)
        {
            await GetDevices();
            CreateHelper<RegisterStudentView> create = new CreateHelper<RegisterStudentView>();
            _result = create.ReadJson("data");
            if (_result != null)
            {
                Section section = await _studentRepo.StudentSection(_result.StudentId);
                TxtFullName.Text = _result.FullName;
                TxtAddress.Text = _result.Address;
                TxtUid.Text = _result.Uid;
                TxtSection.Text = section.SectionName;
            }
        }

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtUid.Text) == false)
            {
                var count = await _studentRepo.StudentCheckUid(TxtUid.Text);
                if (count == 0)
                {
                    await _studentRepo.StudentUpdateUid(_result.StudentId, TxtUid.Text);
                    MessageBox.Show("Save complete");
                }
                else
                {
                    MessageBox.Show("This card is already used");
                }
            }
            else
            {
                MessageBox.Show("Please swipe the smart card");
            }
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
            if (IsRegister)
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
                    $"Connected to card\r\nPC/SC device class: {cardIdentification.PcscDeviceClass}\r\nCard name: {cardIdentification.PcscCardName}";

                if (cardIdentification.PcscDeviceClass == DeviceClass.StorageClass &&
                    (cardIdentification.PcscCardName == CardName.MifareStandard1K ||
                     cardIdentification.PcscCardName == CardName.MifareStandard4K))
                {
                    LblLog.Content = "MIFARE Standard/Classic card detected";

                    TxtUid.Text = BitConverter.ToString(await localCard.GetUid());
                }
            }
            catch (Exception ex)
            {
                LblLog.Content = "HandleCard Exception: " + ex.Message;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Window p = Window.GetWindow((DependencyObject)sender);
            if(p != null)
            {
                p.Close();
            }
        }
    }
}
