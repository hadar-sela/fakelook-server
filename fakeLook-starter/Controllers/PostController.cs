
﻿using fakeLook_models.Models;
<<<<<<< HEAD
=======
using fakeLook_starter.Interfaces;
﻿using fakeLook_dal.Data;
>>>>>>> 37a7d907c5fdcb171e2bffa8230672ab057f7872
using fakeLook_starter.Filters;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repository;

        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<PostsController>
        [HttpGet]
        [TypeFilter(typeof(GetUserActionFilter))]
        public IEnumerable<Post> Get()
        {
            Request.RouteValues.TryGetValue("user", out var obj);
            var user = obj as User;
            var p = _repository.GetAll();
            return p;
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public Post Get(int id)
        {
            return _repository.GetById(id);
        }

        // POST api/<PostsController>
        [HttpPost]
        [TypeFilter(typeof(GetUserActionFilter))]
        [Authorize]
        public void Post([FromBody] Post value)
        {
            Request.RouteValues.TryGetValue("user", out var obj);
            var user = obj as User;
            if(user!=null)
            value.UserId = user.Id;
            value.Date = DateTime.Now;
  

            _ = _repository.Add(value);
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Post value)
        {
            _ = _repository.Edit(value);
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
