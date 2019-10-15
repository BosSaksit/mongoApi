using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mongoApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;


namespace mongoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        IMongoCollection<student> StudentCollection { get; set; }

        public StudentController()
        {
            var client = new MongoClient("mongodb+srv://test:test123456@cluster0-tbroz.mongodb.net/test?retryWrites=true&w=majority");
            var db = client.GetDatabase("students");
            StudentCollection = db.GetCollection<student>("student");
        }
  
        
        [HttpGet]
        public ActionResult<IEnumerable<student>> GetStudentAll()
        {           
            var result = StudentCollection.Find(it => true).ToList();
           return result;
        }

        [HttpGet("{id}")]
        public ActionResult<student> GetStudentById(string id)
        {
            var GetById = StudentCollection.Find(it => true).ToList();
          return GetById.FirstOrDefault(it => it.IdStudent == id.ToString()); 
            
        }

        [HttpPost]
        public student AddStudent([FromBody] student data)
        {
            var id = Guid.NewGuid().ToString();
            var item = new student
            {
               _id = id,
               IdStudent = id,
               NameStudent = data.NameStudent,
               SubjectStudent = data.SubjectStudent,
               ScoreStudent = data.ScoreStudent,
               DateStudent = DateTime.Now,
            };
            
            StudentCollection.InsertOne(item);
            return item;
        }

        [HttpPut("{id}")]
        public student EditStudent(string id, [FromBody] student data)
        {
           var dataUpdate = Builders<student>.Update
           .Set(it => it.NameStudent, data.NameStudent)
           .Set(it => it.SubjectStudent, data.SubjectStudent)
           .Set(it => it.ScoreStudent, data.ScoreStudent)
           .Set(it => it.DateStudent, DateTime.Now);

           StudentCollection.UpdateOne(it => it.IdStudent == id.ToString(),dataUpdate);

           var GetById = StudentCollection.Find(it => true).ToList();          
            return GetById.FirstOrDefault(it => it.IdStudent == id);

        }

        [HttpDelete("{id}")]
        public void DeleteStudent(string id)
        {
             var filter = Builders<student>.Filter.Eq("IdStudent", id);
             var result = StudentCollection.DeleteOne(filter);
        }

    }
}