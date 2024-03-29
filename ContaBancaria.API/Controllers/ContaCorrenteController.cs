﻿using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Service.Dto;
using ContaBancaria.Service.Services;
using ContaBancaria.Service.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaBancaria.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private ContaCorrenteService service = new ContaCorrenteService();

        [HttpPost]
        public IActionResult Post([FromBody] ContaCorrente item)
        {
            try
            {
                service.Post<ContaCorrenteValidator>(item);

                return new ObjectResult(item.Id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] ContaCorrente item)
        {
            try
            {
                service.Put<ContaCorrenteValidator>(item);

                return new ObjectResult(item);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);

                return new NoContentResult();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return new ObjectResult(service.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return new ObjectResult(service.Get(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("debitar")]
        public IActionResult Debitar([FromBody] ObjetoEntrada objetoEntrada)
        {
            try
            {
                var retorno = service.Debitar(objetoEntrada.id, objetoEntrada.valor);

                return new ObjectResult(retorno);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("creditar")]
        public IActionResult Creditar([FromBody] ObjetoEntrada objetoEntrada)
        {
            try
            {
                var retorno = service.Creditar(objetoEntrada.id, objetoEntrada.valor);

                return new ObjectResult(retorno);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
