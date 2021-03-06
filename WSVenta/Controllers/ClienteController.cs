﻿

//https://www.youtube.com/watch?v=jrjRQGZVXGE
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WSVenta.Models;
using WSVenta.Models.Request_ViewModels;
using WSVenta.Models.Response;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    var lst = db.Cliente.OrderByDescending(d => d.Id).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Exito - 200";
                    oRespuesta.Data = lst;
                    return Ok(oRespuesta);
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
                oRespuesta.Mensaje = "400_" + ex;
                return NotFound(oRespuesta);
            }
        }

        [HttpPost]
        public IActionResult Add(ClienteRequest oClienteRequest)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCleinte = new Cliente();
                    oCleinte.Cliente1 = oClienteRequest.Nombre;
                    db.Cliente.Add(oCleinte);
                    db.SaveChanges();
                    //oRespuesta.Mensaje = "Exito - Post_200";
                    oRespuesta.Exito = 1;
                    // return Ok(oRespuesta);
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
                //oRespuesta.Mensaje = "400__" + ex;
                //return NotFound(oRespuesta);
            }
            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(ClienteRequest oClienteRequest)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCleinte = db.Cliente.Find(oClienteRequest.id);
                    oCleinte.Cliente1 = oClienteRequest.Nombre;
                    db.Entry(oCleinte).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Mensaje = "Put_201";
                    oRespuesta.Exito = 1;
                    return Ok(oRespuesta);
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
                oRespuesta.Mensaje = "400__" + ex;
                return NotFound(oRespuesta);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCleinte = db.Cliente.Find(id);
                    db.Remove(oCleinte);
                    db.SaveChanges();
                    oRespuesta.Mensaje = "Delete - _200";
                    oRespuesta.Exito = 1;
                    return Ok(oRespuesta);
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
                oRespuesta.Mensaje = "400__" + ex;
                return NotFound(oRespuesta);
            }

        }
    }
}