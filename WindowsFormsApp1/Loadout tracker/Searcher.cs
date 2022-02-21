using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using Data;
using System.Drawing;

namespace Loadout_tracker
{
    public class Searcher
    {
        public Game Game { get; private set; }
        private List<PowerTemplate> PowerTemplates;
        private PowerTemplateDAL PowerTemplateDAL;
        public Searcher()
        {
            PowerTemplateDAL = new PowerTemplateDAL();
            PowerTemplates = new List<PowerTemplate>();
            
            foreach (PowerTemplateDTO powerTemplateDTO in PowerTemplateDAL.GetAllPowerTemplates())
            {
                PowerTemplates.Add(new PowerTemplate(powerTemplateDTO));
            }
        }

        public Game GetLoadouts(byte[] image)
        {
            
            foreach(PowerTemplate powerTemplate in PowerTemplates)
            {

            }
            return Game;
        }

        private Point findImage(Image<Bgr, byte> image1, Image<Bgr, byte> image2, Point point1, Point point2)
        {
            double Threshold = 0.70; //set it to a decimal value between 0 and 1.00, 1.00 meaning that the images must be identical

            Image<Gray, float> Matches = image1.MatchTemplate(image2, TemplateMatchingType.CcoeffNormed);

            for (int y = point1.Y; y < point2.Y; y++)
            {
                for (int x = point1.X; x < point2.X; x++)
                {
                    if (Matches.Data[y, x, 0] >= Threshold)
                    {
                        
                        return new Point(x, y);
                    }
                }
            }
            return new Point(0, 0);
        }



        private Point getoffset(Image<Bgr, byte> Image1, Image<Bgr, byte> Image2)
        {
            double Threshold = 0.70;

            Image<Gray, float> Matches = Image1.MatchTemplate(Image2, TemplateMatchingType.CcoeffNormed);

            for (int y = 0; y < Matches.Data.GetLength(0); y++)
            {
                for (int x = 0; x < Matches.Data.GetLength(1); x++)
                {
                    if (Matches.Data[y, x, 0] >= Threshold)
                    {

                        return new Point(x, y);
                    }
                }
            }
            return new Point(0, 0);
        }
    }
    
}
