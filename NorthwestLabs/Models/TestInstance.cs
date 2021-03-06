﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using NorthwestLabs.Models;

namespace NorthwestLabs.Models
{
    [Table("TestInstance")]
    public class TestInstance
    {
        [Key]
        [Display(Name = "Test Instance ID")]
        public int TestInstanceID { get; set; }

        [Display(Name = "Test Type")]
        public virtual int TestTypeID { get; set; }
        public virtual TestType TestType { get; set; }

        [Display(Name = "Assay ID")]
        public int AssayID { get; set; }

        [Display(Name = "Sequence ID")]
        public int SequenceID { get; set; }

        [Display(Name = "Test Required")]
        public bool Required { get; set; }

        [Display(Name = "Approved by Customer")]
        public bool Approved { get; set; }

        [Display(Name = "Date Completed")]
        public DateTime DateCompleted { get; set; }

        [Display(Name = "Base Price")]
        public Decimal BasePrice { get; set; }

        [Display(Name = "Date Scheduled")]
        public DateTime DateScheduled { get; set; }

        [Display(Name = "Needs to Be Rerun")]
        public bool Rerun { get; set; }

        [Display(Name = "Comments on Rerun")]
        public String RerunComments { get; set; }

        [Display(Name = "Success")]
        public bool Success { get; set; }
    }
}

//@model IEnumerable<NorthwestLabs.Models.TestInstance>

//@{
//    ViewBag.Title = "Index";
//}

//<h2>Index</h2>

//<p>
//    @Html.ActionLink("Create New", "Create")
//</p>
//<table class="table">
//    <tr>
//        <th>
//            @Html.DisplayNameFor(model => model.TestType.TestTypeDesc)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.AssayID)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.SequenceID)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.Required)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.Approved)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.DateCompleted)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.BasePrice)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.DateScheduled)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.Rerun)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.RerunComments)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.Success)
//        </th>
//        <th></th>
//    </tr>

//@foreach(var item in Model)
//{
//    < tr >
//        < td >
//            @Html.DisplayFor(modelItem => item.TestType.TestTypeDesc)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.AssayID)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.SequenceID)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.Required)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.Approved)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.DateCompleted)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.BasePrice)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.DateScheduled)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.Rerun)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.RerunComments)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.Success)
//        </ td >
//        < td >
//            @Html.ActionLink("Edit", "Edit", new { id = item.TestInstanceID }) |
//            @Html.ActionLink("Details", "Details", new { id = item.TestInstanceID }) |
//            @Html.ActionLink("Delete", "Delete", new { id = item.TestInstanceID })
//        </ td >
//    </ tr >
//}

//</table>
