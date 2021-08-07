using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BiometricManagement.Dto;
using BiometricManagement.Interface;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using FileHelpers;
using MassTransit;
using Microsoft.Extensions.Logging;
using NetMQ;
using NetMQ.Sockets;

namespace BiometricManagement.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ILogger<GenericRepository> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        public GenericRepository(IPublishEndpoint publishEndpoint, ILogger<GenericRepository> logger,
        IMapper mapper)
        {
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

    public async Task GenerateAttendanceRecordAsync()
    {
        // await GetRecordFromCSV();
        using (var client = new RequestSocket())
        {
            try
            {
                client.Connect("tcp://127.0.0.1:6666");
                client.SendFrame("GenerateRecord");
                var msg = client.ReceiveFrameString();
                if (msg != null)
                {
                    _logger.LogInformation("Generating csv");
                    await GetRecordFromCSV();
                }

            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

        }
    }

    public async Task<int> GetRecordFromCSV()
    {
        try
        {
            var engine = new FileHelperEngine<FileHelperAttendanceLogDto>();


            var result = engine.ReadFile("/home/staz/Documents/Portal/Services/BiometricManagement/Scripts/attendanceLog.csv");
            
           
            await UploadDate(result);
            return 1;
        }
        catch (Exception)
        {
            _logger.LogInformation("exception");
            return 0;
        }

    }

    public Task UploadDate(IEnumerable<FileHelperAttendanceLogDto> model)
    {
        var mapResult = _mapper.Map<IEnumerable<FileHelperAttendanceLogDto>,IEnumerable<GetAttendanceRecordEventDto>>(model);

        GetAttendanceRecordEvent result = new GetAttendanceRecordEvent{
            GetAttendanceRecord=mapResult
        };
        
        _logger.LogInformation("success");
        _logger.LogInformation(result.GetAttendanceRecord.ToString());
        _publishEndpoint.Publish(result);

        return Task.FromResult(1);
        
    }
}
}