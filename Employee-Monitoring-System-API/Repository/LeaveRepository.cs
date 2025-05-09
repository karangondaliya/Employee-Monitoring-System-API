﻿using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using Microsoft.CodeAnalysis;

namespace Employee_Monitoring_System_API.Repository
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly AppDbContext _context;

        public LeaveRepository(AppDbContext context)
        {
            _context = context;
        }

        public LeaveRequest Add(LeaveRequest leaveRequest)
        {
            leaveRequest.StartDate = leaveRequest.StartDate.ToUniversalTime();
            leaveRequest.EndDate = leaveRequest.EndDate.ToUniversalTime();
            _context.LeaveRequests.Add(leaveRequest);
            _context.SaveChanges();
            return leaveRequest;
        }

        public LeaveRequest Delete(int id)
        {
            LeaveRequest lr = _context.LeaveRequests.Find(id);
            if (lr != null)
            {
                _context.LeaveRequests.Remove(lr);
                _context.SaveChanges();
            }
            return lr;
        }

        public IEnumerable<LeaveRequest> GetAllLeaveRequests()
        {
            return _context.LeaveRequests.ToList();
        }

        public IEnumerable<LeaveRequest> GetByUserId(int id)
        {
            return _context.LeaveRequests.Where(l => l.UserId == id).ToList();
        }

        public LeaveRequest GetLeaveRequest(int id)
        {
            return _context.LeaveRequests.Find(id);
        }

        public LeaveRequest Update(LeaveRequest leaveRequestChanges)
        {
            var lr = _context.LeaveRequests.Find(leaveRequestChanges.LeaveRequestId);
            if (lr != null)
            {
                _context.Entry(lr).CurrentValues.SetValues(leaveRequestChanges);
                _context.SaveChanges();
                return lr;
            }
            return null;
        }
    }
}
