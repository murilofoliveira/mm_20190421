using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MindMiners.Application.Interface;
using MindMiners.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MindMiners.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirplaneController : ControllerBase
    {
        private readonly IAppAirplane AppAirplane;

        public AirplaneController(IAppAirplane appAirplane)
        {
            AppAirplane = appAirplane;
        }


        public ActionResult Index()
        {
            return Ok(AppAirplane.Listar());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Airplane airplane)
        {
            this.AppAirplane.Adicionar(airplane);

            return RedirectToAction(nameof(Index));

        }
    }
}