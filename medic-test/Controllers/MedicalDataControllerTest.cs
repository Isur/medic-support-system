using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using medic_api.Controllers.MedicalData;
using medic_api.DAL.Models;
using medic_api.DAL.Repository.MedicalData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace medic_test.Controllers
{
    public class MedicalDataControllerTest
    {
        [Fact]
        public void MedicalDataControllerGet()
        {
            var newId = new Guid();
            var mock = new Mock<IMedicalDataRepository>();
            var controller = new MedicalDataController(mock.Object);
            var data = controller.Get(newId.ToString());
            mock.Verify(x => x.GetMedicalData(newId.ToString()), Times.Once);
            Assert.IsType<ActionResult<MedicalData>>(data);
        }
        
        [Fact]
        public void MedicalDataControllerGetList()
        {
            var mock = new Mock<IMedicalDataRepository>();
            var controller = new MedicalDataController(mock.Object);
            var data = controller.Get();
            mock.Verify(x => x.GetMedicalDataList(), Times.Once);
            Assert.IsType<ActionResult<List<MedicalData>>>(data);
        }
        
        [Fact]
        public void MedicalDataControllerGetListByUser()
        {
            var newId = new Guid();
            var mock = new Mock<IMedicalDataRepository>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, newId.ToString()),
            }, "mock"));
            
            var controller = new MedicalDataController(mock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            var data = controller.GetMyMedicalData();
            mock.Verify(x => x.GetMedicalDataListByUser(newId.ToString()), Times.Once);
            Assert.IsType<ActionResult<List<MedicalData>>>(data);
        }
    }
}