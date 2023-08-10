using FinalTodo.Models;

namespace FinalTodo.Data
{
    public class DbInitializer 
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

        
            var userTasks = new UserTask[]
            {
            new UserTask{Title="ff",Description="Chemistry",DueDate=DateTime.Parse("2005-09-01"),IsCompleted = true},
            new UserTask{Title="ff",Description="Microeconomics",DueDate=DateTime.Parse("2005-09-01"),IsCompleted = true},
            new UserTask{Title="ff",Description="Macroeconomics",DueDate=DateTime.Parse("2005-09-01"),IsCompleted = true},
            new UserTask{Title="ff",Description="Calculus",DueDate=DateTime.Parse("2005-09-01"),IsCompleted = true},
            new UserTask{Title="ff",Description="Trigonometry",DueDate=DateTime.Parse("2005-09-01"),IsCompleted = true},

            };
            foreach (UserTask t in userTasks)
            {
                context.UserTasks.Add(t);
            }
            context.SaveChanges();


        }
    }
}
