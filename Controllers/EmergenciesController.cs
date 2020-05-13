﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Diplom.DataModels;
using Microsoft.AspNetCore.Http;
using Diplom.Services;

namespace Diplom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmergenciesController : ControllerBase
    {
        private readonly IEmergencyService _emergencyService;

        public EmergenciesController(IEmergencyService emergencyService)
        {
            _emergencyService = emergencyService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Emergency>> GetAsync()
        {
            return await _emergencyService.ListAsync();
        }

        [HttpGet("{EmergencyName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Emergency>> GetAsync(string EmergencyName)
        {
            return await _emergencyService.ListAsync(EmergencyName);
        }
    }
}
