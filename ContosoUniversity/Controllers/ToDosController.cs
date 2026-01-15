using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class ToDosController : BaseController
    {
        // GET: ToDos
        public ActionResult Index()
        {
            var todos = new List<ToDo>();
            
            try
            {
                var connection = db.Database.GetDbConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_GetAllToDos";
                    command.CommandType = CommandType.StoredProcedure;
                    
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            todos.Add(new ToDo
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                IsCompleted = reader.GetBoolean(reader.GetOrdinal("IsCompleted")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                CompletedDate = reader.IsDBNull(reader.GetOrdinal("CompletedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CompletedDate"))
                            });
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error loading todos: {ex.Message} | Stack: {ex.StackTrace}");
                TempData["ErrorMessage"] = "Unable to load todos. Try again, and if the problem persists see your system administrator.";
            }
            
            return View(todos);
        }

        // GET: ToDos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ToDo todo = null;
            
            try
            {
                var connection = db.Database.GetDbConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_GetToDoById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ID", id));
                    
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            todo = new ToDo
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                IsCompleted = reader.GetBoolean(reader.GetOrdinal("IsCompleted")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                CompletedDate = reader.IsDBNull(reader.GetOrdinal("CompletedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CompletedDate"))
                            };
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error loading todo details: {ex.Message} | ID: {id} | Stack: {ex.StackTrace}");
                TempData["ErrorMessage"] = "Unable to load todo details. Try again, and if the problem persists see your system administrator.";
                return RedirectToAction("Index");
            }

            if (todo == null)
            {
                return HttpNotFound();
            }
            
            return View(todo);
        }

        // GET: ToDos/Create
        public ActionResult Create()
        {
            var todo = new ToDo
            {
                CreatedDate = DateTime.Today,
                IsCompleted = false
            };
            return View(todo);
        }

        // POST: ToDos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,IsCompleted,CreatedDate,CompletedDate")] ToDo todo)
        {
            try
            {
                // Validate CreatedDate
                if (todo.CreatedDate == DateTime.MinValue || todo.CreatedDate == default(DateTime))
                {
                    ModelState.AddModelError("CreatedDate", "Please enter a valid created date.");
                }

                if (ModelState.IsValid)
                {
                    var connection = db.Database.GetDbConnection();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_CreateToDo";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Title", todo.Title));
                        command.Parameters.Add(new SqlParameter("@Description", (object)todo.Description ?? DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@IsCompleted", todo.IsCompleted));
                        command.Parameters.Add(new SqlParameter("@CreatedDate", todo.CreatedDate));
                        command.Parameters.Add(new SqlParameter("@CompletedDate", (object)todo.CompletedDate ?? DBNull.Value));
                        
                        connection.Open();
                        var newId = command.ExecuteScalar();
                        connection.Close();
                        
                        Trace.TraceInformation($"Created ToDo with ID: {newId}");
                    }
                    
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error creating todo: {ex.Message} | Title: {todo?.Title} | Stack: {ex.StackTrace}");
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            
            return View(todo);
        }

        // GET: ToDos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ToDo todo = null;
            
            try
            {
                var connection = db.Database.GetDbConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_GetToDoById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ID", id));
                    
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            todo = new ToDo
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                IsCompleted = reader.GetBoolean(reader.GetOrdinal("IsCompleted")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                CompletedDate = reader.IsDBNull(reader.GetOrdinal("CompletedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CompletedDate"))
                            };
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error loading todo for edit: {ex.Message} | ID: {id} | Stack: {ex.StackTrace}");
                TempData["ErrorMessage"] = "Unable to load todo. Try again, and if the problem persists see your system administrator.";
                return RedirectToAction("Index");
            }

            if (todo == null)
            {
                return HttpNotFound();
            }
            
            return View(todo);
        }

        // POST: ToDos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,IsCompleted,CreatedDate,CompletedDate")] ToDo todo)
        {
            try
            {
                // Validate CreatedDate
                if (todo.CreatedDate == DateTime.MinValue || todo.CreatedDate == default(DateTime))
                {
                    ModelState.AddModelError("CreatedDate", "Please enter a valid created date.");
                }

                if (ModelState.IsValid)
                {
                    var connection = db.Database.GetDbConnection();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_UpdateToDo";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ID", todo.ID));
                        command.Parameters.Add(new SqlParameter("@Title", todo.Title));
                        command.Parameters.Add(new SqlParameter("@Description", (object)todo.Description ?? DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@IsCompleted", todo.IsCompleted));
                        command.Parameters.Add(new SqlParameter("@CreatedDate", todo.CreatedDate));
                        command.Parameters.Add(new SqlParameter("@CompletedDate", (object)todo.CompletedDate ?? DBNull.Value));
                        
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        
                        Trace.TraceInformation($"Updated ToDo with ID: {todo.ID}");
                    }
                    
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error editing todo: {ex.Message} | ID: {todo?.ID} | Title: {todo?.Title} | Stack: {ex.StackTrace}");
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            
            return View(todo);
        }

        // GET: ToDos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ToDo todo = null;
            
            try
            {
                var connection = db.Database.GetDbConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_GetToDoById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ID", id));
                    
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            todo = new ToDo
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                IsCompleted = reader.GetBoolean(reader.GetOrdinal("IsCompleted")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                CompletedDate = reader.IsDBNull(reader.GetOrdinal("CompletedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CompletedDate"))
                            };
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error loading todo for delete: {ex.Message} | ID: {id} | Stack: {ex.StackTrace}");
                TempData["ErrorMessage"] = "Unable to load todo. Try again, and if the problem persists see your system administrator.";
                return RedirectToAction("Index");
            }

            if (todo == null)
            {
                return HttpNotFound();
            }
            
            return View(todo);
        }

        // POST: ToDos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var connection = db.Database.GetDbConnection();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_DeleteToDo";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ID", id));
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    
                    Trace.TraceInformation($"Deleted ToDo with ID: {id}");
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error deleting todo: {ex.Message} | ID: {id} | Stack: {ex.StackTrace}");
                TempData["ErrorMessage"] = "Unable to delete the todo. Try again, and if the problem persists see your system administrator.";
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Base class will dispose db
            }
            base.Dispose(disposing);
        }
    }
}
