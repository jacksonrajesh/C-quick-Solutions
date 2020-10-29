using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestExcelXml
{
    class Program
    {
        static void Main(string[] args)
        {
            List<HouseExcel> values = File.ReadAllLines("C:\\Users\\jmuduli\\Documents\\house-prices-advanced-regression-techniques\\train.csv")
                                          .Skip(1)
                                          .Select(v => HouseExcel.FromCsv(v))
                                          .ToList();
            foreach (var item in values)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("C:\\Users\\jmuduli\\Documents\\house-prices-advanced-regression-techniques\\house-prices-advanced-regression-techniques.xml");
                XmlNode root = doc.DocumentElement;
                XmlNode myNode = root.SelectSingleNode("appraisal/data");
                foreach (var nodeForm in myNode)
                {
                    var formNode = (XmlNode)nodeForm;
                    var attr = formNode.Attributes;
                    if (formNode.Attributes["name"].Value == "cnasum_14")
                    {
                        foreach (var sectionNode in formNode)
                        {
                            var xmlsectionNode = (XmlNode)sectionNode;
                            if (xmlsectionNode.Attributes["name"].Value == "SUBJECT")
                            {
                                xmlsectionNode.SelectSingleNode("tag[@name='SITE_ZONE_CLASS_DESC.1']").InnerText = item.MSSubClass + " " + item.SITE_ZONE_CLASS_DESC_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='SITE_AREA.1']").InnerText = item.SITE_AREA_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='OSIM_ROAD_PAVED.1']").InnerText = item.OSIM_ROAD_PAVED_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='SITE_SHAPE.1']").InnerText = item.SITE_SHAPE_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='SITE_TOPOGRAPHY.1']").InnerText = item.SITE_TOPOGRAPHY_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='UTIL_TELEPHONE.1']").InnerText = item.UTIL_TELEPHONE_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='UTIL_WATER_PUB.1']").InnerText = item.UTIL_WATER_PUB_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='WATER_HEATER_GAS.1']").InnerText = item.WATER_HEATER_GAS_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='UTIL_STORM_SEWER.1']").InnerText = item.UTIL_STORM_SEWER_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='UTIL_CABLEVISION.1']").InnerText = item.UTIL_CABLEVISION_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='UTIL_SAN_SEWER_PUB.1']").InnerText = item.UTIL_SAN_SEWER_PUB_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='UTIL_ELECTRIC_OVERHEAD.1']").InnerText = item.UTIL_ELECTRIC_OVERHEAD_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='SITE_HB_USE.1']").InnerText = item.SITE_HB_USE_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='DESIGN_ONE_STORY.1']").InnerText = item.DESIGN_ONE_STORY_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='INTERIOR_COND_AVERAGE.1']").InnerText = item.INTERIOR_COND_AVERAGE_1;
                                xmlsectionNode.SelectSingleNode("tag[@name='GDES_YEAR_BUILT.1']").InnerText = item.GDES_YEAR_BUILT_1;

                                //likewise for other rows
                            }
                        }
                    }

                }

                doc.Save("C:\\Users\\jmuduli\\Documents\\house-prices-advanced-regression-techniques\\ACI" + item.Id + ".xml");
                //creating 5 xml file for testing 
                if (item.Id == "5")
                {
                    break;
                }
            }

        }

    }

    public class HouseExcel
    {
        public string Id { get; set; }
        public string MSSubClass { get; set; }
        public string SITE_ZONE_CLASS_DESC_1 { get; set; }
        public string SITE_AREA_1 { get; set; }
        public string OSIM_ROAD_PAVED_1 { get; set; }
        public string SITE_SHAPE_1 { get; set; }
        public string SITE_TOPOGRAPHY_1 { get; set; }
        public string UTIL_TELEPHONE_1 { get; set; }
        public string UTIL_WATER_PUB_1 { get; set; }
        public string WATER_HEATER_GAS_1 { get; set; }
        public string UTIL_STORM_SEWER_1 { get; set; }
        public string UTIL_CABLEVISION_1 { get; set; }
        public string UTIL_SAN_SEWER_PUB_1 { get; set; }
        public string UTIL_ELECTRIC_OVERHEAD_1 { get; set; }
        public string SITE_HB_USE_1 { get; set; }
        public string DESIGN_ONE_STORY_1 { get; set; }
        public string INTERIOR_COND_AVERAGE_1 { get; set; }
        public string GDES_YEAR_BUILT_1 { get; set; }

        public static HouseExcel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            HouseExcel houseExcel = new HouseExcel();
            houseExcel.Id = values[0].ToString();
            houseExcel.MSSubClass = values[1].ToString();
            houseExcel.SITE_ZONE_CLASS_DESC_1 = values[2].ToString();
            houseExcel.SITE_AREA_1 = values[4].ToString();
            houseExcel.OSIM_ROAD_PAVED_1 = values[5].ToString();
            houseExcel.SITE_SHAPE_1 = values[7].ToString();
            houseExcel.SITE_TOPOGRAPHY_1 = values[8].ToString();
            houseExcel.UTIL_TELEPHONE_1 = "allpub".Contains(values[9].ToString().ToLower()) ? "x" : string.Empty;
            houseExcel.UTIL_WATER_PUB_1 = "allpub, nosewr".Contains(values[9].ToString().ToLower()) ? "x" : string.Empty;
            houseExcel.WATER_HEATER_GAS_1 = "allpub, nosewr, nosewa".Contains(values[9].ToString().ToLower()) ? "x" : string.Empty;
            houseExcel.UTIL_STORM_SEWER_1 = "allpub".Contains(values[9].ToString().ToLower()) ? "x" : string.Empty;
            houseExcel.UTIL_CABLEVISION_1 = "allpub".Contains(values[9].ToString().ToLower()) ? "x" : string.Empty;
            houseExcel.UTIL_SAN_SEWER_PUB_1 = "allpub".Contains(values[9].ToString().ToLower()) ? "x" : string.Empty;
            houseExcel.UTIL_ELECTRIC_OVERHEAD_1 = "allpub, nosewr, nosewa, elo".Contains(values[9].ToString().ToLower()) ? "x" : string.Empty;
            houseExcel.SITE_HB_USE_1 = values[15].ToString();
            houseExcel.DESIGN_ONE_STORY_1 = values[16].ToString() == "1Story" ? "x" : string.Empty;
            houseExcel.INTERIOR_COND_AVERAGE_1 = values[18].ToString();
            houseExcel.GDES_YEAR_BUILT_1 = values[19].ToString();

            return houseExcel;
        }
    }
}
