using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using System;

namespace Heuristics.TechEval.Web.Controllers {

	public class MembersController : Controller {

		private readonly DataContext _context;

		public MembersController() {
			_context = new DataContext();
		}

		public ActionResult List() {
			var allMembers = _context.Members.ToList();

			return View(allMembers);
		}

		[HttpPost]
		public ActionResult New(NewMember data) {

			if (_context.Members.Any(x => x.Email.ToLower() == data.Email.ToLower()))
            {
				var mod = ModelState.First(c => c.Key == "Email");
				mod.Value.Errors.Add("Email is already in use.");    
			}

			if (!ModelState.IsValid)
			{
				List<string> modelErrors = new List<string>();
				foreach (var modelState in ModelState.Values)
				{
					foreach (var modelError in modelState.Errors)
					{
						modelErrors.Add(modelError.ErrorMessage);
					}
				}
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json(new { success = false, responseText = modelErrors[0] });
			}

			var newMember = new Member {
				Name = data.Name,
				Email = data.Email
			};

			_context.Members.Add(newMember);
			_context.SaveChanges();

			return Json(JsonConvert.SerializeObject(newMember));
		}

		public ActionResult Edit(int Id)
		{
			var member = _context.Members.Where(x => x.Id == Id).FirstOrDefault();
			EditMember editMember = new EditMember()
			{
				Id = member.Id,
				Name = member.Name,
				Email = member.Email
			};
			return View(editMember);
		}


		[HttpPost]
		public ActionResult Edit(EditMember editMember)
		{

			if (_context.Members.Any(x => x.Email.ToLower() == editMember.Email.ToLower() && x.Id != editMember.Id))
			{
				var mod = ModelState.First(c => c.Key == "Email");
				mod.Value.Errors.Add("Email is already in use.");
			}

			if (!ModelState.IsValid)
				return View(editMember);

			var entry = _context.Members.FirstOrDefault(x => x.Id == editMember.Id);
			entry.Name = editMember.Name;
			entry.Email = editMember.Email;
			_context.SaveChanges();

			return RedirectToAction("List");
		}
	}
}