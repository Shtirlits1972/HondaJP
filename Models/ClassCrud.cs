using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using HondaJP.Models.Dto;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace HondaJP.Models
{
    public class ClassCrud
    {
        private static string strConn = Ut.GetMySQLConnect();
        public static List<CarTypeInfo> GetListCarTypeInfo(string body_code, string body_num, string lang = "EN")
        {
            if(!String.IsNullOrEmpty(lang))
            {
                strConn = Ut.GetMySQLConnect(lang);
            }

            List<CarTypeInfo> list = null;


                try
                {
                    #region strCommand
                    string strCommand = " SELECT " +
                                        " k.pos, " +
                                        " k.model, " +
                                        " k.body, " +
                                        " k.modification, " +
                                        " k.transmission, " +
                                        " k.door, " +
                                        " k.engine " +
                                        " FROM ksrbhmh k " +
                                        " WHERE k.body_code = @body_code " +
                                        " AND k.body_num_from <= @body_num AND " +
                                        " ((k.body_num_to IS NULL) OR " +
                                        " (k.body_num_to >= @body_num))";
                    #endregion

                    using (IDbConnection db = new MySqlConnection(strConn))
                    {
                        list = db.Query<CarTypeInfo>(strCommand, new { body_code, body_num }).ToList();
                    }
                }
                catch
                {

                }

            return list;
        }
        public static List<ModelCar> GetModelCars(string lang = "EN")
        {
            if (!String.IsNullOrEmpty(lang))
            {
                strConn = Ut.GetMySQLConnect(lang);
            }

            List<ModelCar> list = null;
            string strCommand = "SELECT id, model,  REPLACE(model, ' ', '-' )  AS 'seo_url' FROM tsmir;";

            try
            {
                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    list = db.Query<ModelCar>(strCommand).ToList();
                }
            }
            catch { }
            return list;
        }
        public static List<PartsGroup> GetPartsGroup(int pos, string lang = "EN")
        {
            if (!String.IsNullOrEmpty(lang))
            {
                strConn = Ut.GetMySQLConnect(lang);
            }

            List<PartsGroup> list = null;
            string strCommand = " SELECT id, `desc` " +
                                     " FROM cat_grptbl c " + 
                                     " WHERE c.catalog IN " + 
                                     " ( " + 
                                     " SELECT " + 
                                     "   k.catalog " + 
                                     "   FROM " + 
                                     "   ksrbhmh k " +
                                     "   WHERE k.pos = @pos " + 
                                     " ); ";

            try
            {
                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    list = db.Query<PartsGroup>(strCommand, new { pos }).ToList();
                }
            }
            catch(Exception ex)
            {
                string Error = ex.Message;
                int o = 0;
            }
            return list;
        }
        public static List<header> GetHeaders()
        {
            List<header> list = new List<header>();

            header header1 = new header { fid = "pos", title = "ИД" };
            list.Add(header1);
            header header2 = new header { fid = "model", title = "Модель" };
            list.Add(header2);
            header header3 = new header { fid = "body", title = "Кузов" };
            list.Add(header3);
            header header4 = new header { fid = "modification", title = "Модификация" };
            list.Add(header4);
            header header5 = new header { fid = "transmission", title = "Трансмиссия" };
            list.Add(header5);
            header header6 = new header { fid = "door", title = "Двери" };
            list.Add(header6);
            header header7 = new header { fid = "engine", title = "Двигатель" };
            list.Add(header7);

            return list;
        }
        public static List<hotspots> GetHotspots(int vehicle_id, string image, string lang = "EN")
        {
            if (!String.IsNullOrEmpty(lang))
            {
                strConn = Ut.GetMySQLConnect(lang);
            }

            List<hotspots> list = null;
            try
            {
                #region strCommand
                string strCommand = " SELECT " +
                                " cic.id, cic.image AS name, cic.label AS number, " +
                                "  cic.x1 AS min_x, " +
                                "  cic.y1 AS min_y, " +
                                "  cic.x2 AS max_x, " +
                                "  cic.y2 AS max_y " +
                                "  FROM " +
                                "  cat_img_coord cic " +
                                "  WHERE cic.catalog IN " +
                                "  (SELECT  k.catalog " +
                                "    FROM ksrbhmh k " +
                                "    WHERE k.pos = @vehicle_id) " +
                                "  AND cic.image = @image; ";
                #endregion

                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    list = db.Query<hotspots>(strCommand, new { vehicle_id, image }).ToList();
                }
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                int o = 0;
            }

            return list;
        }
        public static List<SpareParts> GetSpareParts(int group_id, int vehicle_id, string lang = "EN")
        {
            if (!String.IsNullOrEmpty(lang))
            {
                strConn = Ut.GetMySQLConnect(lang);
            }

            List<SpareParts> list = null;

            try
            {
                #region strCommand
                string strCommand = "  SELECT " +
                                    "  c.id, c.`desc` AS name, CONCAT(b.img_name, '.' , b.img_ext) AS image_id " +
                                    "  FROM cat_plmst c, cat_blktytbl b " +
                                    " WHERE c.catalog IN " +
                                    "  (SELECT k.catalog " +
                                    "    FROM " +
                                    "    ksrbhmh k " +
                                    "    WHERE k.pos = @vehicle_id " +
                                    "  )  AND " +
                                    "  c.node_code1 = b.node_code1  AND " +
                                    "  c.node_code2 = b.node_code2 AND " +
                                    "  c.node_code3 = b.node_code3 AND " +
                                    "  b.id = @group_id;";
                #endregion

                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    list = db.Query<SpareParts>(strCommand, new { group_id, vehicle_id }).ToList();
                }
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                int o = 0;
            }

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].hotspots = GetHotspots(vehicle_id, list[i].image_id);
                }
            }

            return list;
        }
        public static List<Filters> GetFilters(int modelId, string lang = "EN")
        {
            if (!String.IsNullOrEmpty(lang))
            {
                strConn = Ut.GetMySQLConnect(lang);
            }

            List<Filters> filters = new List<Filters>();

            try
            {
                #region Кузов
                List<string> bodyList = new List<string>();

                string bodyCom = "  SELECT DISTINCT k.body FROM ksrbhmh k  " +
                                  "  WHERE k.model IN  " +
                                  "  (SELECT t.model FROM tsmir t  " +
                                  "      WHERE ID = @modelId ); ";
                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    bodyList = db.Query<string>(bodyCom, new { modelId }).ToList();
                }
                Filters bodyF = new Filters { Id = "1", name = "Кузов" };
                List<values> bodyVal = new List<values>();

                for (int i = 0; i < bodyList.Count; i++)
                {
                    values v1 = new values { Id = bodyList[i], name = bodyList[i] };
                    bodyVal.Add(v1);
                }

                bodyF.values = bodyVal;
                filters.Add(bodyF);
                #endregion

                #region Тип трансмиссии
                List<string> transmisList = new List<string>();
                string transmisCom = " SELECT DISTINCT k.transmission FROM ksrbhmh k  " +
                                          "  WHERE k.model IN " +
                                          "  (SELECT t.model FROM tsmir t " +
                                          "      WHERE ID = @modelId )";

                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    transmisList = db.Query<string>(transmisCom, new { modelId }).ToList();
                }
                Filters transmisF = new Filters { Id = "2", name = "Трансмиссия" };
                List<values> transmisVal = new List<values>();

                for (int i = 0; i < transmisList.Count; i++)
                {
                    values v1 = new values { Id = transmisList[i], name = transmisList[i] };
                    transmisVal.Add(v1);
                }

                transmisF.values = transmisVal;
                filters.Add(transmisF);
                #endregion

                #region  Двери
                List<string> doorList = new List<string>();
                string doorCom = "SELECT DISTINCT k.door FROM ksrbhmh k  " +
                "  WHERE k.model IN " +
                "  (SELECT t.model FROM tsmir t " +
                "      WHERE ID = @modelId); ";

                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    doorList = db.Query<string>(doorCom, new { modelId }).ToList();
                }
                Filters doorF = new Filters { Id = "3", name = "Двери" };
                List<values> doorVal = new List<values>();

                for (int i = 0; i < doorList.Count; i++)
                {
                    values v1 = new values { Id = doorList[i], name = doorList[i] };
                    doorVal.Add(v1);
                }

                doorF.values = doorVal;
                filters.Add(doorF);
                #endregion

                #region  engine Двигатель
                List<string> engineList = new List<string>();
                string engineCom =  "  SELECT DISTINCT k.engine FROM ksrbhmh k  " +
                                    "  WHERE k.model IN  " +
                                    " (SELECT t.model FROM tsmir t  " +
                                    "  WHERE ID = @modelId ); ";
                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    engineList = db.Query<string>(engineCom, new { modelId }).ToList();
                }
                Filters engineF = new Filters { Id = "4", name = "Двигатель" };
                List<values> engineVal = new List<values>();

                for (int i = 0; i < engineList.Count; i++)
                {
                    values v1 = new values { Id = engineList[i], name = engineList[i] };
                    engineVal.Add(v1);
                }

                engineF.values = engineVal;
                filters.Add(engineF);
                #endregion

                #region  Модификация
                List<string> modificationList = new List<string>();
                string modificationCom =  "  SELECT DISTINCT k.modification FROM ksrbhmh k " +
                                          "  WHERE k.model IN " +
                                          "  (SELECT t.model FROM tsmir t " +
                                          "  WHERE ID = @modelId); ";
                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    modificationList = db.Query<string>(modificationCom, new { modelId }).ToList();
                }
                Filters modificationF = new Filters { Id = "5", name = "Модификация" };
                List<values> modificationVal = new List<values>();

                for (int i = 0; i < modificationList.Count; i++)
                {
                    values v1 = new values { Id = modificationList[i], name = modificationList[i] };
                    modificationVal.Add(v1);
                }

                modificationF.values = modificationVal;
                filters.Add(modificationF);
                #endregion

            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                int o = 0;
            }

            return filters;
        }
        public static List<Sgroups> GetSgroups(string catalog, int pos, int size, string lang = "EN")
        {
            if (!String.IsNullOrEmpty(lang))
            {
                strConn = Ut.GetMySQLConnect(lang);
            }

            List<Sgroups> list = null;

            try
            {
                #region strCommand
                string strCommand = "  SELECT " +
                                    "  c.id, c.desc1 AS name " +
                                    "  FROM cat_blktytbl c " +
                                    "  WHERE (c.pos BETWEEN  @pos AND (@size - 1) ) " +
                                    "  AND c.catalog = @catalog  ;  ";

                #endregion

                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    list = db.Query<Sgroups>(strCommand, new {  pos, size, catalog }).ToList();
                }
            }
            catch(Exception ex)
            {
                string Errror = ex.Message;
                int y = 0;
            }


            return list;
        }
        public static List<CarTypeInfo> GetListCarTypeInfoFilterCars(int modelId, string body, string modification, string door, string transmission, string engine, string lang = "EN")
        {
            if (!String.IsNullOrEmpty(lang))
            {
                strConn = Ut.GetMySQLConnect(lang);
            }

            List<CarTypeInfo> list = null;

                try
                {
                    #region strCommand
                    string strCommand =   " SELECT " +
                                          " k.pos, " +
                                          " k.model, " +
                                          " k.body, " +
                                          " k.modification, " +
                                          " k.transmission, " +
                                          " k.door, " +
                                          " k.engine " +
                                          " FROM ksrbhmh k " +
                                          " WHERE k.model IN " +
                                          " (SELECT t.model FROM tsmir t " +
                                          "  WHERE t.ID = @modelId ) " +
                                          " AND k.body = @body " +
                                          " AND k.modification = @modification " +
                                          " AND k.door = @door " +
                                          " AND k.transmission = @transmission " +
                                          " AND k.engine = @engine ; ";
                #endregion

                     using (IDbConnection db = new MySqlConnection(strConn))
                    {
                        list = db.Query<CarTypeInfo>(strCommand, new { modelId, body, modification, door, transmission, engine }).ToList();
                    }
                }
                catch(Exception ex)
                {
                    string Errror = ex.Message;
                    int y = 0;
                }
            
            return list;
        }

    }
}
