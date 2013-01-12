using DependencyScopeTracingDisposeBug.Models.Entities;
using GenericRepository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DependencyScopeTracingDisposeBug.Services {
    
    public class LoggerService : ILoggerService {

        private readonly IEntityRepository<HttpApiLogRecord> _httpApiLogRecordRepo;
        public LoggerService(IEntityRepository<HttpApiLogRecord> httpApiLogRecordRepo) {

            _httpApiLogRecordRepo = httpApiLogRecordRepo;
        }

        public void Log(HttpApiLogRecord httpApiLogRecord) {

            _httpApiLogRecordRepo.Add(httpApiLogRecord);
            _httpApiLogRecordRepo.Save();
        }
    }
}