using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using MySql.Data.MySqlClient;

namespace Data
{
    public class PowerTemplateDAL : IPowerTemplatContainer
    {
        private string connectionString = "server=localhost;user=root;database=dbddb2;port=3306;password='';SslMode=none";
        MySqlConnection connection;

        public PowerTemplateDAL()
        {
            connection = new MySqlConnection(connectionString);
        }
        public List<PowerTemplateDTO> GetAllPowerTemplates()
        {
            List<PowerTemplateDTO> TemplateDTOs = new List<PowerTemplateDTO>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM `template` WHERE `Type` = 'Power';";
                var cmd = new MySqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PowerTemplateDTO powerTemplateDTO = new PowerTemplateDTO();

                    powerTemplateDTO.Id = reader.GetInt32(0);
                    powerTemplateDTO.Template = (byte[])reader.GetValue(1);
                    TemplateDTOs.Add(powerTemplateDTO);
                }
                connection.Close();
            }
            catch
            {

            }
            return TemplateDTOs;
        }
    }
}
