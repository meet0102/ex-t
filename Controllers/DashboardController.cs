﻿using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()

        {
            //last 7 day
            //DateTime StartDate = DateTime.Today.AddDays(-6);
            //DateTime EndDate = DateTime.Today;

            List<Transaction> SelectedTransaction = await _context.Transactions
                .Include(x => x.Category)
                //.Where(y => y.Date >= StartDate && y.Date <= EndDate)
                .ToListAsync();

            //Total Income
            int TotalIncome = SelectedTransaction
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("C0");  
            //Total Expense
            int TotalExpense = SelectedTransaction
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

            //Balance
            int balance = TotalIncome - TotalExpense;
            ViewBag.Balance = balance.ToString("C0");
            return View();
        }
    }
}
