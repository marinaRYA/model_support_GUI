using System;
using System.Collections.Generic;
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

namespace model_support_GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            
        }
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void StartSimulationButton_Click(object sender, RoutedEventArgs e)
        {
            int callsCount, waitTime, operatorsCount;
            double intensity;
            int totalCallsProcessed = 0;
            int successfulCallsProcessed = 0;
            double averageWaitingTime = 0;
            if (int.TryParse(CallsCountTextBox.Text, out callsCount) && (callsCount>0) && (callsCount < 1000) &&
                int.TryParse(WaitTimeTextBox.Text, out waitTime) && (waitTime > 1) && (waitTime < 120) &&
                double.TryParse(IntensityTextBox.Text, out intensity) && (intensity > 0.1) && (intensity < 1000) &&
                int.TryParse(OperatorsCountTextBox.Text, out operatorsCount) && (operatorsCount > 0) && (operatorsCount < 1000))
            {
                CallGenerator callGenerator = new CallGenerator(4, 5);
                List<Request> requests = callGenerator.GenerateCall(callsCount,intensity);
                List <Operator> operators = new List <Operator>();
                for (int i = 0; i<operatorsCount; i++)
                {
                    Operator op = new Operator("Operator" + (i + 1).ToString());
                    operators.Add(op);
                }
                CallCenterSimulation callCenter = new CallCenterSimulation(operators, requests);
                callCenter.Simulate(waitTime);
                ResultDataGrid.ItemsSource = requests;
                foreach (Operator oper in operators)
                {
                    totalCallsProcessed += oper.calls;
                    successfulCallsProcessed += oper.successCalls;
                }
                foreach (Request req in requests) {
                    averageWaitingTime += req.Delay;
                    req.ConvertMinutesToTimeString();
                    req.ConvertBool();
                }   
                averageWaitingTime /= callsCount;
                MessageBox.Show($"Количество принятых звонков: {totalCallsProcessed}\nКоличество успешно обработанных звонков: {successfulCallsProcessed}\nСреднее время ожидания: {averageWaitingTime} минут");
                int requiredOperators = ErlangB.CalculateRequiredOperators(intensity, 0.1);
                ResultTextBlock.Text = requiredOperators.ToString();
            }
            else
            {
                // Если в TextBox введены некорректные значения, показываем сообщение об ошибке
                MessageBox.Show("Некорректные значения в одном или нескольких полях.");
            }

        }

    }
}
