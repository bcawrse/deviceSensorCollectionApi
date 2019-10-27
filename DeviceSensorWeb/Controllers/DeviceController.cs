using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeviceSensorWeb.CustomAttributes;
using DeviceSensorWeb.Services;
using DeviceSensorWeb.Models;

namespace DeviceSensorWeb.Controllers
{
    [Authorize]
    public class DeviceController : Controller
    {
        private DeviceService _deviceService;

        public DeviceController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        // GET: Device
        public ActionResult Index()
        {
            var devices = _deviceService.Get();

            var deviceVM = new DevicesViewModel()
            {
                Devices = devices
            };

            return View(deviceVM);
        }

        public ActionResult Details(string deviceId)
        {
            var device = _deviceService.Get(deviceId);

            return View(device);
        }

        // GET: Device/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Device/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Device/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Device/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Device/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Device/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Device/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}