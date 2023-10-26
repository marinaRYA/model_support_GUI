using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model_support_GUI
{
    public class Request
    {
        public int ArrivalTime { get; set; } // Время поступления звонка
        public int ProcessingTime { get; set; } // Время длительности
        public int Type { get; set; } // Тип заявки
        public bool IsProcessedSuccessfully { get; set; } // Успешность принятия заявки
        
        public double Prob_success { get; set; } // Успешность обработки заявки
        public int Delay { get; set; }
        public string IsAccept { get; set; }
        public string ArrivalTimeformat { get; set; }
        public Request(int arrivalTime, int processingTime, int type, double prob_succ)
        {
            ArrivalTime = arrivalTime;
            ProcessingTime = processingTime;
            Type = type;
            Prob_success = prob_succ;
            IsProcessedSuccessfully = false; // По умолчанию заявка не обработана
           
            Delay = 0;
        }
        public void ConvertMinutesToTimeString()
        {
            int days = ArrivalTime / (24 * 60); // количество дней
            int hours = (ArrivalTime % (24 * 60)) / 60; // количество часов
            int minutes = ArrivalTime % 60; // количество минут

            ArrivalTimeformat= $"{days:D2}:{hours:D2}:{minutes:D2}";
        }
        public void ConvertBool()
        {
            IsAccept = IsProcessedSuccessfully ? "Accept" : "No accept";
        }

    }
}
