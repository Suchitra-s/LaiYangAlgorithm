using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaiYang_Algorithm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int count = 0;
        private int p1, p2, totaltime, redprocessid, redtime;
        private int fromPid1, toPid1, sendTime1, receiveTime1, value1;
        public MainWindow()
        {
            InitializeComponent();
            //Algorithm algo = new Algorithm();
            //Initialize(algo);
            //algo.Execute();
            MessageToSend = new ObservableCollection<Message>();
            this.DataContext = this;
        }

        public void Initialize(Algorithm algo)
        {
            int count = 0;
            // Process Values

            Process pp1 = new Process();
            pp1.ProcessId = 1;
            pp1.InitialValue = p1;
            pp1.Balance = p1;
            pp1.ColorCode = ConsoleColor.White;

            Process pp2 = new Process();
            pp2.ProcessId = 2;
            pp2.InitialValue = p2;
            pp2.Balance = p2;
            pp2.ColorCode = ConsoleColor.White;

            algo.Processes.Add(pp1);
            algo.Processes.Add(pp2);


            algo.totalTimeSlots = totaltime;
            algo.RedInitiatorPId = redprocessid;
            algo.RedInitiatorTimeSlot = redtime;

            algo.InitializeChannels(MessageToSend.ToList<Message>());

        }
        private ObservableCollection<Message> _messageToSend;

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Algorithm algo = new Algorithm();
            Initialize(algo);
            algo.Execute();

            Process p1 = algo.Processes.Where(x => x.ProcessId == 1).FirstOrDefault();
            p1State.Text = p1.Balance.ToString();
            Process p2 = algo.Processes.Where(x => x.ProcessId == 2).FirstOrDefault();
            p2State.Text = p2.Balance.ToString();

            c12Sent.Text = algo.TotalP1Sent.ToString();
            c12Received.Text = algo.TotalP1Received.ToString();
            c21Sent.Text = algo.TotalP2Sent.ToString();
            c21Received.Text = algo.TotalP2Received.ToString();
            total.Text = ((p1.Balance + p2.Balance) + (algo.TotalP1Sent - algo.TotalP2Received) + (algo.TotalP2Sent - algo.TotalP1Received)).ToString();

            results.Visibility = Visibility.Visible;
        }

        public ObservableCollection<Message> MessageToSend
        {
            get
            {
                return _messageToSend;
            }
            set
            {
                _messageToSend = value;
                OnPropertyChanged("MessageToSend");
            }
        }

        private void btnAddMessage_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateValues())
                messageGrid.Visibility = Visibility.Visible;
            else
                MessageBox.Show("Please Enter Integer values in all Fields");
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (!(ValidateValues() && ValidateGridValues()))
            {
                MessageBox.Show("Please Enter Integer values in all Fields");
                messageGrid.Visibility = Visibility.Visible;
            }
            else
            {
                messageGrid.Visibility = Visibility.Hidden;
                AddValueToGrid();

                fromPId.Text = string.Empty;
                toPId.Text = string.Empty;
                sendTime.Text = string.Empty;
                receiveTime.Text = string.Empty;
                value.Text = string.Empty;

            }
        }

        public bool ValidateValues()
        {
           
            if (string.IsNullOrEmpty(p1Initial.Text) && string.IsNullOrEmpty(p2Initial.Text) &&
                string.IsNullOrEmpty(totalTimeSlots.Text) && string.IsNullOrEmpty(redProcessId.Text) && string.IsNullOrEmpty(redTime.Text))
                return false;
            if (!(int.TryParse(p1Initial.Text, out p1) && int.TryParse(p2Initial.Text, out p2) && int.TryParse(totalTimeSlots.Text, out totaltime) 
                && int.TryParse(redProcessId.Text, out redprocessid) && int.TryParse(redTime.Text, out redtime)))
                return false;
            else
                return true;
        }

        public bool ValidateGridValues()
        {

            if (string.IsNullOrEmpty(fromPId.Text) && string.IsNullOrEmpty(toPId.Text) &&
                string.IsNullOrEmpty(sendTime.Text) && string.IsNullOrEmpty(receiveTime.Text) && string.IsNullOrEmpty(value.Text))
                return false;
            if (!(int.TryParse(fromPId.Text, out fromPid1) && int.TryParse(toPId.Text, out toPid1) && int.TryParse(sendTime.Text, out sendTime1)
                && int.TryParse(receiveTime.Text, out receiveTime1) && int.TryParse(value.Text, out value1)))
                return false;
            else
                return true;
        }

        public void AddValueToGrid()
        {
            Message message = new Message();
            message.MessageId = count++;
            message.SendingProcessId = fromPid1;
            message.ReceivingProcessId = toPid1;
            message.SentTime = sendTime1;
            message.ReceivedTime = receiveTime1;
            message.Value = value1;
            _messageToSend.Add(message);
        }


        #region Public Properties
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Methods
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
