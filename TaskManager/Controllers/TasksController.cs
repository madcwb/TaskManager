using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Repository.Interface;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {


        private readonly ITaskListRepository _taskListRepository;

        public TasksController(ITaskListRepository taskListRepository)
        {
            this._taskListRepository = taskListRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskList>>> GetTasks()
        {
            try
            {
                return Ok(await _taskListRepository.GetTaskList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TaskList>> GetTask(int id)
        {
            try
            {
                var result = await _taskListRepository.GetTaskList(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskList task)
        {
            try
            {
                if (id != task.Id)
                    return BadRequest("Task ID mismatch");


                var taskListToUpdate = await _taskListRepository.UpdateTaskList(task);

                if (taskListToUpdate == null)
                    return NotFound($"TaskList with Id = {task.Id} not found");

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }


        }


        [HttpPost]
        public async Task<ActionResult<TaskList>> PostTask(TaskList task)
        {

            try
            {
                if (task == null)
                    return BadRequest();

                var createdTaskList = await _taskListRepository.AddTaskList(task);

                return CreatedAtAction(nameof(GetTask),
                    new { id = createdTaskList.Id }, createdTaskList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var taskListToDelete = await _taskListRepository.GetTaskList(id);

                if (taskListToDelete == null)
                {
                    return NotFound($"TaskList with Id = {id} not found");
                }

                _taskListRepository.DeleteTaskList(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

    }
}
