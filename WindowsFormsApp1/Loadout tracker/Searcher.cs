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
using Loadout_tracker.Properties;
using System.ComponentModel;

namespace Loadout_tracker
{
    
    public class Searcher
    {
        private List<PowerTemplate> PowerTemplates;
        private PowerTemplateDAL PowerTemplateDAL;

        private double Score;

        private Point Statuslocation;
        private Area Power;
        public Searcher()
        {
            PowerTemplateDAL = new PowerTemplateDAL();
            PowerTemplates = new List<PowerTemplate>();

            Statuslocation = new Point(637, 13);
            Power =  new Area(new Point(409, 537), new Point(448, 576));
            
            foreach (PowerTemplateDTO powerTemplateDTO in PowerTemplateDAL.GetAllPowerTemplates())
            {
                PowerTemplates.Add(new PowerTemplate(powerTemplateDTO));
            }
        }

        public Game GetLoadouts(Image<Bgr, byte> image)
        {
            Image<Bgr, byte> resized = image;
            Game game = new Game();
            //loop true all the scales
            double Scale = 1;
            for (double i = 0.7; i < 1.5; i = i + 0.05)
            {
                double score = 0;
                double bestscore = score;
                resized = image.Resize((int)(image.Width * i), (int)(image.Height * i), Inter.Linear);
                score = findImage(resized, new Image<Bgr, byte>(Resources.scale_template), new Area(new Point(0,0),new Point(resized.Width,resized.Height/3)));
                if(score > bestscore)
                {
                    Scale = i;
                }
            }
            image = image.Resize((int)(image.Width * Scale), (int)(image.Height * Scale), Inter.Linear);
            Point offset = getoffset(image, new Image<Bgr, byte>(Resources.scale_template));
            if (Score > 0.8)
            {
                foreach (PowerTemplate powerTemplate in PowerTemplates)
                {
                    game.KLoadout = new KLoadout();
                    Area powerArea = Power.offset(offset.X,offset.Y);
                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                    Bitmap template = (Bitmap)tc.ConvertFrom(powerTemplate.Template);
                    if (findImage(image, new Image<Bgr, byte>(template),powerArea) > 0.7){
                        game.KLoadout.Power = powerTemplate.Id;
                    }
                }
            }
            return game;
        }

        private double findImage(Image<Bgr, byte> image1, Image<Bgr, byte> image2, Area area)
        {
            double score = 0;
            double TopThreshold = 0.85; //set it to a decimal value between 0 and 1.00, 1.00 meaning that the images must be identical
            double bottemThreshold = 0.7;
            Image<Gray, float> Matches = image1.MatchTemplate(image2, TemplateMatchingType.CcoeffNormed);

            for (int y = area.Spoint.Y; y < area.Epoint.Y - image2.Height; y++)
            {
                for (int x = area.Spoint.X; x < area.Epoint.X - image2.Width; x++)
                {
                    double temp = Matches.Data[y, x, 0];
                    if(temp > TopThreshold) return temp;
                    if (temp >= bottemThreshold)
                    {
                        if(temp > Score) score = temp;
                    }
                }
            }
            return score;
        }



        private Point getoffset(Image<Bgr, byte> Image1, Image<Bgr, byte> Image2)
        {
            Score = 0;
            double Threshold = 0.80;

            Image<Gray, float> Matches = Image1.MatchTemplate(Image2, TemplateMatchingType.CcoeffNormed);

            for (int y = 0; y < Matches.Data.GetLength(0); y++)
            {
                for (int x = 0; x < Matches.Data.GetLength(1); x++)
                {
                    double temp = Matches.Data[y, x, 0];
                    if (temp >= Threshold)
                    {
                        Score = temp;
                        return new Point(Statuslocation.X - x, Statuslocation.Y - y);
                    }
                }
            }
            return new Point(0, 0);
        }
    }

    class Area
    {
        public Point Spoint { get; private set; }
        public Point Epoint { get; private set; }

        public Area(Point point1, Point point2)
        {
            Spoint = point1;
            Epoint = point2;
        }

        public Area offset(int x,int y)
        {
            Area area = new Area(new Point(Spoint.X- x, Spoint.Y - y), new Point(Epoint.X - x, Epoint.Y - y));
            return area;
        }
    }
    
}
