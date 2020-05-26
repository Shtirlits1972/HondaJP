using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HondaJP.Models.Dto;
using HondaJP.Models;
using System.IO;
using System.Configuration;

namespace HondaJP.Controllers
{
    public class ApiController : Controller
    {
        [Route("/vehicle/vin")]
        public IActionResult GetListCarTypeInfo(string body_code, string body_num, string lang)
        {
            List<CarTypeInfo> list = ClassCrud.GetListCarTypeInfo(body_code, body_num, lang); 
            List<header> headerList = ClassCrud.GetHeaders();

            var result = new
            {
                headers = headerList,
                items = list,
                cntitems = list.Count,
                page = 1
            };

            return Json(result);
        }

        [Route("/image")]
        public IActionResult GetImage(string image_id)
        {

            string FilderImagePath = Ut.GetImagePath();  //"wwwroot/image/";
            string fullPath = FilderImagePath + image_id;

            if(System.IO.File.Exists(fullPath))
            {
                byte[] file = System.IO.File.ReadAllBytes(fullPath);
                return Ok(file);
            }

            return NotFound("Картинка не найдена.");
        }

        [Route("/models")]
        public IActionResult GetModels(string lang)
        {
            List<ModelCar> list = ClassCrud.GetModelCars(lang);
            return Json(list);
        }

        [Route("/mgroups")]
        public IActionResult GetPartsGroups(int pos, string lang)
        {
            List<PartsGroup> list = ClassCrud.GetPartsGroup(pos, lang);
            return Json(list);
        }

        [Route("/vehicle")]
        public IActionResult GetSpareParts(int group_id, int vehicle_id,string lang)
        {
            List<SpareParts> list = ClassCrud.GetSpareParts(group_id, vehicle_id, lang);   
            return Json(list);
        }

        [Route("/vehicle/sgroups")]
        public IActionResult GetSgroups(string catalog, int pos, int size, string lang = "EN")
        {  
            List<Sgroups> list = ClassCrud.GetSgroups(catalog, pos, size, lang);  
            return Json(list);
        }

        [Route("/ﬁlters")]
        public IActionResult GetFilters(int modelId, string lang)
        {
            List<Filters> list = ClassCrud.GetFilters(modelId, lang);   
            return Json(list);
        }

        [Route("/ﬁlter-cars")]
        public IActionResult GetListCarTypeInfoFilterCars(int modelId, string [] param, int page=1, int page_size=10, string lang = "EN")
        {
            if(param.Length == 5)
            {
                List<header> headerList = ClassCrud.GetHeaders();

                string body = param[0];
                string modification = param[1];
                string door = param[2];
                string transmission = param[3];
                string engine = param[4];

                List<CarTypeInfo> list = ClassCrud.GetListCarTypeInfoFilterCars(modelId, body, modification, door, transmission, engine, lang); // 

                list = list.Skip((page - 1) * page_size).Take(page_size).ToList();

                var result = new
                {
                    headers = headerList,
                    items = list,
                    cntitems = list.Count,
                    page = page
                };

                return Json(result);
            }

            return NotFound("Проверьте параметры запроса!");
        }

        [Route("/GetLang")]
        public IActionResult GetLangList()
        {
            List<string> listLang = Ut.GetIndexOfConnectString();
            return Json(listLang);
        }
    }
}