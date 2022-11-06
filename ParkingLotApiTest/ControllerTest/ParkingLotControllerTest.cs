﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using ParkingLotApi.Controllers;
using ParkingLotApi.Dto;
using ParkingLotApi.Services;
using Xunit.Abstractions;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotControllerTest
    {
        [Fact]
        public async Task Should_create_parking_lot_whith_mock_service()
        {
            //Given
            var parkingLotService = new Mock<IParkingLotService>();
            parkingLotService.Setup(m => m.AddParkingLot(It.IsAny<ParkingLotDto>())).ReturnsAsync(1);
            var parkingLotController = new ParkingLotController(parkingLotService.Object);
            //When
            var actionResult = await parkingLotController.CreateParkingLot(new ParkingLotDto()
            {
                Name = "SLB",
                Capacity = 10,
                Location = "TUSPark",
            });
            //Then
            var createdResult = Assert.IsType<CreatedResult>(actionResult.Result);
            var dtoResult = Assert.IsType<ParkingLotDto>(createdResult.Value);
            Assert.Equal("SLB",dtoResult.Name);
            Assert.Equal(201,createdResult.StatusCode);
        }
    }
}
