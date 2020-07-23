using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InversalTranslatorTemp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la ruta del archivo");
            string Path = Console.ReadLine();
            Converter conv = new Converter(@"C:\Users\USER\Desktop\TempExample.txt");
            
            Console.WriteLine("Conversion terminada \n Ingrese la ruta donde desea guardar el archivo");
            Path = Console.ReadLine();
            Path += @"\TemperaturesOutput.txt";
            if (File.Exists(Path))
            {
                Console.WriteLine("Ya existe el archivo en esta ruta, desea sobre escrbirlo? [S/N]");
                string ans = Console.ReadLine();
                if (ans.ToUpper() == "S")
                {
                    File.Delete(Path);
                    conv.Save(Path);
                    Console.WriteLine("All done, Presione cualquier tecla para salir");
                }
                else
                {   /*
                    Console.WriteLine("Desea agregar la información al final? [S/N]");
                    ans = Console.ReadLine();
                    if (ans.ToUpper() == "S")
                    {
                        File.
                    }*/
                    Console.WriteLine("Ok, presione cualquier tecla para salir");
                }
            }
            else
            {
                conv.Save(Path);
                Console.WriteLine("All done, Presione cualquier tecla para salir");
            }

            Console.ReadKey();

        }     
    }

    public class Converter
    {
        public class NoFile : Exception { };
        public class InvalidGrade : Exception { };

        public List<Temperature> Temps = new List<Temperature>();
        public Converter(string rute)
        {
            if (!File.Exists(rute))
            {
                throw new NoFile();
            }
            string[] Data = File.ReadAllLines(rute);

            for (int i = 1; i < Data.Length; i++)
            {
                string item = Data[i];
                string[] values = item.Split('\t');
                Temperature temp = new Temperature(double.Parse(values[0]), values[1], values[2]);
                Temps.Add(temp);
            }

            
        }
        public void Save(string Path)
        {
            TextWriter tw = new StreamWriter(Path);
            tw.WriteLine("Temp\tGradeO\tGradeD\tResult");
            foreach (Temperature temp in Temps)
            {
                string OutPut = string.Format("{0}\t{1}\t{2}\t{3}", temp.Temp, temp.OriginGrade, temp.DestGrade, temp.FinalTemp);
                tw.WriteLine(OutPut);
            }
            tw.Close();
        }
        public class Temperature
        {

            public enum ValidTemps { C, F, K, None };

            public double Temp { private set; get; }
            public double FinalTemp { private set; get; }
            private double internalTemp;
            public ValidTemps OriginGrade { private set; get; }
            public ValidTemps DestGrade { private set; get; }

            public Temperature(double temp, string Origin, string Dest)
            {
                Temp = Math.Round(temp,2);
                if (ValidGrade(Origin) == ValidTemps.None)
                    throw new InvalidGrade();
                OriginGrade = ValidGrade(Origin);
                if (ValidGrade(Dest) == ValidTemps.None)
                    throw new InvalidGrade();
                DestGrade = ValidGrade(Dest);

                Convert();
            }
            private ValidTemps ValidGrade(string grade)
            {
                switch (grade.ToUpper())
                {
                    case "C":
                        return ValidTemps.C;
                    case "F":
                        return ValidTemps.F;
                    case "K":
                        return ValidTemps.K;
                }
                return ValidTemps.None;
            }

            private void Convert()
            {
                ToGeneric();
                if (ValidTemps.C == DestGrade)
                {
                    FinalTemp = internalTemp;
                    return;
                }
                if (DestGrade == ValidTemps.F)
                    CtoF();
                else
                    CtoK();
            }

            private void ToGeneric()
            {
                if (OriginGrade == ValidTemps.C)
                    internalTemp = Temp;
                else if (OriginGrade == ValidTemps.F)
                    internalTemp = (Temp - 32) * ((double)5 / (double)9);
                else if(OriginGrade == ValidTemps.K)
                    internalTemp = Temp - 273.15;
                internalTemp = Math.Round(internalTemp, 2);
            }

            private void CtoF()
            {
                FinalTemp = Math.Round((internalTemp * ((double)9 / (double)5)) + 32,2);
            }
            private void CtoK()
            {
                FinalTemp = Math.Round(internalTemp + 273.15,2);
            }

        }
    }
}
