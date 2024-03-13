using Microsoft.AspNetCore.Mvc;
using StackOverflow.Datos;
using StackOverflow.Models;
using StackOverflow.Utilidades;

namespace StackOverflow.Controllers
{
    public class Controllers : Controller
    {
        PostRepository PTmethod = new PostRepository();
        CategoryRepository CTmethod = new CategoryRepository();
        
        public IActionResult Listar()
        {
            return View(PTmethod.GetModels());
        }

        public IActionResult InsertPost()
        {
            var modelViws = new ModelViews();
            ModelViews.categories = CTmethod.GetModels();
            modelViws.newPost = new Post();
            return View(modelViws);
        }

        [HttpPost]
        public IActionResult InsertPost(ModelViews model)
        {
            var valid = new Validaciones();
            var motor = new MotorValidaciones();

            ModelViews.Validation = motor.ValidacionesAdd(model.newPost, valid.PostValidations);

            if (ModelViews.Validation && PTmethod.InsertModel(model.newPost))
            {
                return RedirectToAction("Listar");
            }
            return View();
        }

        public IActionResult ShowPost(int id)
        {
            return View(PTmethod.GetPost(id));
        }

        public IActionResult UpdatePost(int id)
        {
            var post = new ModelViews() { newPost = PTmethod.GetPost(id)};
            ModelViews.categories = CTmethod.GetModels();
            return View(post);
        }

        [HttpPost]
        public IActionResult UpdatePost(ModelViews model)
        {
            if (PTmethod.UpdatePost(model.newPost))
            {
                return RedirectToAction("Listar");
            }
            return View();
        }

        public IActionResult UpVote(int id)
        {
            var model = PTmethod.GetPost(id);
            if (PTmethod.SetVote(model, model.VoteUp))
            {
                return RedirectToAction("Listar");
            }
            return View();
        }
        public IActionResult DownVote(int id)
        {
            var valid = new Validaciones();
            var motor = new MotorValidaciones();
            var model = PTmethod.GetPost(id);

            var valido = motor.ValidacionesVote(model, valid.ValidationsVote);

            if( valido && PTmethod.SetVote(model, model.VoteDown))
            {
                return RedirectToAction("Listar");
            }
            return RedirectToAction("Listar");
        }

        public IActionResult ELiminar(int id)
        {
            return View(PTmethod.GetPost(id));
        }

        [HttpPost]
        public IActionResult ELiminar(Post model)
        {
            if (PTmethod.DeleteModel(model))
            {
                return RedirectToAction("Listar");
            }
            return View();
        }
    }
}
