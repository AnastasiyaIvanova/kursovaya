using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UlskDel.Models;
using iTextSharp.text.html;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.end;
using System.Xml.Linq;
using iTextSharp.tool.xml.parser;
using System.Text;

namespace UlskDel.Controllers
{
    public class OrdersController : Controller
    {
        private OrderContext db = new OrderContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,Sender,Receiver,Address_Sender,Address_Receiver,Phone_Sender,Phone_Receiver,Date,Time,Weight,Length,Width,Height,Who_pay")] Order order)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
                int id = user.Id;
                order.UserId = id;
                order.Status = "Обрабатывается";
                order.Price = 0;
                order.Print = false;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Sender,Receiver,Address_Sender,Address_Receiver,Phone_Sender,Phone_Receiver,Date,Time,Status,Weight,Length,Width,Height,Who_pay,Price,UserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                if (order.Status!="обрабатывается")
                {
                    order.Print = true;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/MyFile.pdf";
            Document DC = new Document();
            DC.SetPageSize(PageSize.A4.Rotate());
            FileStream FS = System.IO.File.Create(Path);
            PdfWriter writer = PdfWriter.GetInstance(DC, FS);
            DC.Open();

            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            PdfPTable table = new PdfPTable(3);//число столбцов
            //Добавим в таблицу общий заголовок
            PdfPCell cell = new PdfPCell(new Phrase("Отправитель ", font));
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1;
            //Убираем границу первой ячейки, чтобы балы как заголовок
            cell.Border = 0;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("ФИО", font)));
            //Фоновый цвет (необязательно, просто сделаем по красивее)
            cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("Телефон", font)));
            //Фоновый цвет (необязательно, просто сделаем по красивее)
            cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("Адрес", font)));
            //Фоновый цвет (необязательно, просто сделаем по красивее)
            cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table.AddCell(cell);
            table.AddCell(new Phrase(order.Sender, font));
            table.AddCell(new Phrase(order.Phone_Sender, font));
            table.AddCell(new Phrase(order.Address_Sender, font));
            //Добавляем таблицу в документ
            DC.Add(table);

            //receiver
            PdfPTable table1 = new PdfPTable(3);//число столбцов
            //Добавим в таблицу общий заголовок
            PdfPCell cell1 = new PdfPCell(new Phrase("Получатель ", font));
            cell1.Colspan = 3;
            cell1.HorizontalAlignment = 1;
            //Убираем границу первой ячейки, чтобы балы как заголовок
            cell1.Border = 0;
            table1.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(new Phrase("ФИО", font)));
            //Фоновый цвет (необязательно, просто сделаем по красивее)
            cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table1.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(new Phrase("Телефон", font)));
            //Фоновый цвет (необязательно, просто сделаем по красивее)
            cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table1.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(new Phrase("Адрес", font)));
            //Фоновый цвет (необязательно, просто сделаем по красивее)
            cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table1.AddCell(cell1);
            table1.AddCell(new Phrase(order.Receiver, font));
            table1.AddCell(new Phrase(order.Phone_Receiver, font));
            table1.AddCell(new Phrase(order.Address_Receiver, font));
            //Добавляем таблицу в документ
            DC.Add(table1);

            //order
            PdfPTable table2 = new PdfPTable(6);//число столбцов
            //Добавим в таблицу общий заголовок
            PdfPCell cell2 = new PdfPCell(new Phrase("Груз ", font));
            cell2.Colspan = 6;
            cell2.HorizontalAlignment = 1;
            //Убираем границу первой ячейки, чтобы балы как заголовок
            cell2.Border = 0;
            table2.AddCell(cell2);
            cell2 = new PdfPCell(new Phrase(new Phrase("Вес", font)));
            cell2.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table2.AddCell(cell2);
            cell2 = new PdfPCell(new Phrase(new Phrase("Длина", font)));
            cell2.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table2.AddCell(cell2);
            cell2 = new PdfPCell(new Phrase(new Phrase("Высота", font)));
            cell2.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table2.AddCell(cell2);
            cell2 = new PdfPCell(new Phrase(new Phrase("Ширина", font)));
            cell2.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table2.AddCell(cell2);
            cell2 = new PdfPCell(new Phrase(new Phrase("Оплата получателем", font)));
            cell2.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table2.AddCell(cell2);
            cell2 = new PdfPCell(new Phrase(new Phrase("Цена", font)));
            cell2.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            table2.AddCell(cell2);
            table2.AddCell(new Phrase(order.Weight.ToString(), font));
            table2.AddCell(new Phrase(order.Length.ToString(), font));
            table2.AddCell(new Phrase(order.Height.ToString(), font));
            table2.AddCell(new Phrase(order.Width.ToString(), font));
            string str = "нет";
            if (order.Who_pay)
            {
                str = "да";
            }
            table2.AddCell(new Phrase(str, font));
            table2.AddCell(new Phrase(order.Price.ToString(), font));
            //Добавляем таблицу в документ
            DC.Add(table2);

            DC.Add(new Paragraph("Я подтверждаю, что информация в накладной является полной и точной. С основными условиями пересылки я ознакомлен(а).", font));
            DC.SetMargins(10, 10, 10, 10);
            DC.Add(new Paragraph("Подпись_________________", font));
            DC.Close();


            //HtmlPipelineContext htmlContext = new HtmlPipelineContext(null);
            //htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
            //ICSSResolver cssresolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(true);
            //IPipeline pipeline =
            // new CssResolverPipeline(cssresolver,
            //  new HtmlPipeline(htmlContext, new PdfWriterPipeline(DC, writer)));

            //XMLWorker worker = new XMLWorker(pipeline, true);
            //XMLParser p = new XMLParser(true, worker, Encoding.Unicode);

            //p.Parse((TextReader)System.IO.File.OpenText(@"C:\Users\user\Videos\Template.html"));
            //DC.Close();
            return RedirectToAction("Index", "LK");
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
