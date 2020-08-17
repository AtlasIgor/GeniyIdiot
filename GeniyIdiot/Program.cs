using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniyIdiot
{
    class Program
    {

        static int GetRandomSelect(int[] statusAsk, int numberQuestions)
        {
            Random randomGenerator = new Random();
            int randomIndex = randomGenerator.Next(0, numberQuestions);
            while (statusAsk[randomIndex] == 0)             // find not used ask 1
            {
                randomIndex = randomGenerator.Next(0, numberQuestions);
            }
            statusAsk[randomIndex] = 0;                     // mark it ask as used 0
            return randomIndex;
        }

        static int CheckRigthNumerInput(int i, string[] questions, int randomIndex)
        {
            bool correctTryParse = false;  // success convert default - false
            int userAnswer = 0;

            while (!correctTryParse)    // while Not correct
            {
                correctTryParse = int.TryParse(Console.ReadLine(), out userAnswer);
                if (!correctTryParse) Console.WriteLine("\n\tПожалуйста, введите целое число! (последовательность цифр от 0 до 9)\n");
            }
            return userAnswer;
        }

        static int GetIndexIndexStatusBrain(double percentsOfQualityAnswers, int numberStatus)
        {
            for (int i = 0; i < numberStatus - 1; i++)
            {
                double leftLim = (double)i / (numberStatus-1);
                double rigthLim = (double)(i + 1) / (numberStatus-1);
                if (leftLim <= percentsOfQualityAnswers && percentsOfQualityAnswers <= rigthLim)
                {
                    if (percentsOfQualityAnswers - leftLim < rigthLim - percentsOfQualityAnswers) return i;
                    else return i + 1;
                }
            }
            return -1;
        }

        static string GetFIO()    // get name (Russian format)
        {
            string FIO;
            Console.WriteLine("Здравствуйте,\n я программа тестирования на статус от \"Гений\" до \"Идиот\"");
            Console.Write("представтесь пожалуйста, введите Ваши Фамилию, Имя, Отчество\n\n\t: ");
            FIO = Console.ReadLine(); // now not verify correct first und last Name und father Name
            Console.WriteLine();
            return FIO;
        }

        static void Main(string[] args)
        {
            int numberQuestions  = 12;
            int numberStatus     = 6;
            string[] questions   = CreateArrayQuestions(numberQuestions);
            int[]    arrayAnswers     = CreateArrayAnswers  (numberQuestions);
            int[]    statusAsk   = CreateArrayStatusQuery(numberQuestions);
            string[] statusBrain = CreateArrayStatus   (numberStatus);
            int countRightAnswers = 0;
            int randomIndex;
            int userAnswer = 0;
            string FIO = GetFIO();          // FIO
            double percentsOfQualityAnswers;
            int numberDiagnos = 0;
            bool repeatTest = true;

            while (repeatTest)
            {
                for (int i = 0; i < numberQuestions; i++)
                {
                    randomIndex = GetRandomSelect(statusAsk, numberQuestions);
                    Console.Write("Вопрос № " + (i + 1) + ".\n" + questions[randomIndex] + " ");  // put Ask
                    userAnswer = CheckRigthNumerInput(i, questions, randomIndex);
                    if (userAnswer == arrayAnswers[randomIndex]) countRightAnswers++;
                }

                percentsOfQualityAnswers = (double)countRightAnswers / numberQuestions;
                numberDiagnos = GetIndexIndexStatusBrain(percentsOfQualityAnswers, numberStatus);
                if (numberDiagnos == -1) Console.WriteLine("что-то пошло не так");

                Console.Write("\nКоличество правильных ответов (" + countRightAnswers + ") в процентном отношении: ");
                Console.WriteLine(percentsOfQualityAnswers * 100 + "%\n" + FIO + ", Ваш диагноз: " + statusBrain[numberDiagnos]);
                Console.Write("Есть жклание повторить? (N - Отказаться / в любом другом случае продолжить):");
                string consoleRead = Console.ReadLine();
                if (consoleRead == "N" || consoleRead == "n" || consoleRead == "Т" || consoleRead == "т") repeatTest = false;
                                // No,                    no                   Т || т (rus keyboard)

                // initial for next repeat
                statusAsk = CreateArrayStatusQuery(numberQuestions);
                statusBrain = CreateArrayStatus(numberStatus);
                questions = CreateArrayQuestions(numberQuestions);
                arrayAnswers = CreateArrayAnswers(numberQuestions);
                countRightAnswers = 0;

            }
        }
    }
}

static string[] CreateArrayQuestions(int numberQuestions)
{
    string[] questions = new string[numberQuestions];
    questions[0] = "Сколько будет два плюс два умноженное на два?";
    questions[1] = "Бревно нужно распилить на 10 частей, сколько надо сделать  распилов?";
    questions[2] = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?";
    questions[3] = "Укол делают каждые полчаса, сколько нужно минут для трех  уколов?";
    questions[4] = "Пять свечей горело, две потухли. Сколько свечей  осталось?";
    questions[5] = "На яблоне висело 8 яблок, половину сьели пионеры, сколько яблок осталось на березе?";
    questions[6] = "учтите, сегодня: " + DateTime.Today.Day + "." + DateTime.Today.Month + "." + DateTime.Today.Year + "\nКакой сегодня день месяца?";
    questions[7] = "учтите, сегодня: " + DateTime.Today.Day + "." + DateTime.Today.Month + "." + DateTime.Today.Year + "\nКакой сегодня номер месяца в году?";
    questions[8] = "учтите, сегодня: " + DateTime.Today.Day + "." + DateTime.Today.Month + "." + DateTime.Today.Year + "\nКакой сегодня год?";
    questions[9] = "Сколько будет дважды два и умножить на два?";
    questions[10] = "2 в 3 степени?";
    questions[11] = "2 в 9 степени?";

        /* Проверка фиксации изменений на гитхаб */

    return questions;
}

static int[] CreateArrayStatusQuery(int numberQuestions)
{
    int[] statusAsk = new int[numberQuestions];
    for (int i = 0; i < numberQuestions; i++) statusAsk[i] = 1;
    return statusAsk;    // init array 1 - not use this ask / 0 - use this ask
}

static int[] CreateArrayAnswers(int numberQuestions)
{
    int[] arrayAnswers = new int[numberQuestions];
    arrayAnswers[0] = 6;
    arrayAnswers[1] = 9;
    arrayAnswers[2] = 25;
    arrayAnswers[3] = 60;
    arrayAnswers[4] = 2;
    arrayAnswers[5] = 0;
    arrayAnswers[6] = DateTime.Today.Day;
    arrayAnswers[7] = DateTime.Today.Month;
    arrayAnswers[8] = DateTime.Today.Year;
    arrayAnswers[9] = 8;
    arrayAnswers[10] = 8;
    arrayAnswers[11] = 512;
    return arrayAnswers;
}

static string[] CreateArrayStatus(int numberStatus)
{
    string[] status = new string[6];
    status[5] = "Гений";
    status[4] = "Талант";
    status[3] = "Нормальный";
    status[2] = "Дурак";
    status[1] = "Кретин";
    status[0] = "Идиот";
    return status;
}
