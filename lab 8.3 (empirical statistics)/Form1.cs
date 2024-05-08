using System.Windows.Forms.DataVisualization.Charting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab_8._3__empirical_statistics_
{
    public partial class Form1 : BaseEditorForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double aProb) &&
                double.TryParse(textBox2.Text, out double bProb) &&
                double.TryParse(textBox3.Text, out double cProb) &&
                double.TryParse(textBox4.Text, out double dProb) &&
                double.TryParse(textBox6.Text, out double count) &&
                aProb > 0 &&
                bProb > 0 &&
                cProb > 0 &&
                dProb > 0 &&
                aProb + bProb + cProb + dProb <= 1)
            {
                var eProb = 1 - (aProb + bProb + cProb + dProb);
                textBox5.Text = Math.Round(eProb, 2).ToString();
                chart1.Series[0].Points.Clear();
                Random random = new();
                int aCount = 0;
                int bCount = 0;
                int cCount = 0;
                int dCount = 0;
                int eCount = 0;
                double current;
                for (int i = 0; i < (int)count; i++)
                {
                    current = random.NextDouble();

                    if (current < aProb)
                    {
                        aCount++;
                        continue;
                    }
                    current -= aProb;

                    if (current < bProb)
                    {
                        bCount++;
                        continue;
                    }
                    current -= bProb;

                    if (current < cProb)
                    {
                        cCount++;
                        continue;
                    }
                    current -= cProb;

                    if (current < dProb)
                    {
                        dCount++;
                        continue;
                    }

                    eCount++;
                }
                var aEmpProb = aCount / count;
                var bEmpProb = bCount / count;
                var cEmpProb = cCount / count;
                var dEmpProb = dCount / count;
                var eEmpProb = eCount / count;

                var average =
                    1 * aProb +
                    2 * bProb +
                    3 * cProb +
                    4 * dProb +
                    5 * eProb;

                var empiricalAverage =
                    1 * aEmpProb +
                    2 * bEmpProb +
                    3 * cEmpProb +
                    4 * dEmpProb +
                    5 * eEmpProb;

                var empiricalAverageRelativeError =
                    Math.Abs(average - empiricalAverage) / average;

                var variance =
                    (Math.Pow(1 - average , 2) / count * aProb) +
                    (Math.Pow(2 - average, 2) / count * bProb) +
                    (Math.Pow(3 - average, 2) / count * cProb) +
                    (Math.Pow(4 - average, 2) / count * dProb) +
                    (Math.Pow(5 - average, 2) / count * eProb);

                var empiricalVariance =
                    (Math.Pow(1 - empiricalAverage, 2) / count * aProb) +
                    (Math.Pow(2 - empiricalAverage, 2) / count * bProb) +
                    (Math.Pow(3 - empiricalAverage, 2) / count * cProb) +
                    (Math.Pow(4 - empiricalAverage, 2) / count * dProb) +
                    (Math.Pow(5 - empiricalAverage, 2) / count * eProb);

                var empiricalVarianceRelativeError =
                    Math.Abs(variance - empiricalVariance) / variance;

                /*var chiSquared =
                    Math.Pow(aCount, 2) / (count * aEmpProb) - count +
                    Math.Pow(bCount, 2) / (count * bEmpProb) - count +
                    Math.Pow(cCount, 2) / (count * cEmpProb) - count +
                    Math.Pow(dCount, 2) / (count * dEmpProb) - count +
                    Math.Pow(eCount, 2) / (count * eEmpProb) - count;*/

                var chiSquared =
                    Math.Pow(aCount - count * aProb, 2) / (count * aProb) +
                    Math.Pow(bCount - count * bProb, 2) / (count * bProb) +
                    Math.Pow(cCount - count * cProb, 2) / (count * cProb) +
                    Math.Pow(dCount - count * dProb, 2) / (count * dProb) +
                    Math.Pow(eCount - count * eProb, 2) / (count * eProb);



                var max = Math.Max(aCount, Math.Max(bCount, Math.Max(cCount, Math.Max(dCount, eCount)))) / count;
                chart1.ChartAreas[0].AxisY.Maximum = max;
                chart1.Series[0].Points.AddXY(1D, aEmpProb);
                chart1.Series[0].Points.AddXY(2D, bEmpProb);
                chart1.Series[0].Points.AddXY(3D, cEmpProb);
                chart1.Series[0].Points.AddXY(4D, dEmpProb);
                chart1.Series[0].Points.AddXY(5D, eEmpProb);




                label6.Text =
                    $"Эмпирические вероятности:" +
                    $"\n\tProb 1: {Math.Round(aEmpProb, 3)};" +
                    $"\n\tProb 2: {Math.Round(bEmpProb, 3)};" +
                    $"\n\tProb 3: {Math.Round(cEmpProb, 3)};" +
                    $"\n\tProb 4: {Math.Round(dEmpProb, 3)};" +
                    $"\n\tProb 5: {Math.Round(eEmpProb, 3)};" +
                    $"\nВыборочное среднее:" +
                    $"\n\tЗначение: {Math.Round(empiricalAverage, 5)};" +
                    $"\n\tПогрешность: {Math.Round(empiricalAverageRelativeError, 5)};" +
                    $"\nДисперсия:" +
                    $"\n\tЗначение: {Math.Round(empiricalVariance, 5)};" +
                    $"\n\tПогрешность: {Math.Round(empiricalVarianceRelativeError, 5)};" +
                    $"\nХи-квадрат: {Math.Round(chiSquared, 5)}";
            }
        }


    }
}
