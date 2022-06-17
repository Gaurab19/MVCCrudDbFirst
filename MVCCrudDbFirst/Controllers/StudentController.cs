
using MVCCrudDbFirst.Context;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace mvcDbFirstLecture.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        db_testEntities dbObj = new db_testEntities();
        public ActionResult Student(tbl_Student obj)
        {
            return View(obj);
        }
        [HttpPost]
        public ActionResult AddStudent(tbl_Student model)
        {
            tbl_Student obj = new tbl_Student();
            if (ModelState.IsValid)
            {
                obj.Id = model.Id;
                obj.Name = model.Name;
                obj.FName = model.FName;
                obj.Email = model.Email;
                obj.Mobile = model.Mobile;
                obj.Description = model.Description;

                if (model.Id > 0)
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.tbl_Student.Add(obj);
                    dbObj.SaveChanges();
                }
                ModelState.Clear();
            }


            return RedirectToAction("StudentList");
        }


        public ActionResult StudentList()
        {
            var res = dbObj.tbl_Student.ToList();
            return View(res);
        }

        public ActionResult Delete(int id)
        {
            var res = dbObj.tbl_Student.Where(x => x.Id == id).First();
            dbObj.tbl_Student.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.tbl_Student.ToList();

            return View("StudentList", list);
        }



    }
}