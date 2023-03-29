using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

public class PdfMerge
{
	private iTextSharp.text.pdf.BaseFont baseFont;

	private bool enablePagination;

	private readonly List<PdfReader> documents;

	private int totalPages;

	public iTextSharp.text.pdf.BaseFont BaseFont
	{
		get
		{
			return this.baseFont;
		}
		set
		{
			this.baseFont = value;
		}
	}

	public List<PdfReader> Documents
	{
		get
		{
			return this.documents;
		}
	}

	public bool EnablePagination
	{
		get
		{
			return this.enablePagination;
		}
		set
		{
			this.enablePagination = value;
			if (value && this.baseFont == null)
			{
				this.baseFont = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "Cp1252", false);
			}
		}
	}

	public PdfMerge()
	{
		this.documents = new List<PdfReader>();
	}

	public void AddDocument(string filename)
	{
		this.documents.Add(new PdfReader(filename));
	}

	public void AddDocument(Stream pdfStream)
	{
		this.documents.Add(new PdfReader(pdfStream));
	}

	public void AddDocument(byte[] pdfContents)
	{
		this.documents.Add(new PdfReader(pdfContents));
	}

	public void AddDocument(PdfReader pdfDocument)
	{
		this.documents.Add(pdfDocument);
	}

	public void Merge(string outputFilename)
	{
		this.Merge(new FileStream(outputFilename, FileMode.Create));
	}

	public void Merge(Stream outputStream)
	{
		if (outputStream == null || !outputStream.CanWrite)
		{
			throw new Exception("OutputStream is not as per prescribed format");
		}
		Document document = null;
		try
		{
			document = new Document();
			PdfWriter instance = PdfWriter.GetInstance(document, outputStream);
			document.Open();
			PdfContentByte directContent = instance.DirectContent;
			if (this.EnablePagination)
			{
				this.documents.ForEach((PdfReader doc) => {
					PdfMerge numberOfPages = this;
					numberOfPages.totalPages = numberOfPages.totalPages + doc.NumberOfPages;
				});
			}
			int num = 1;
			foreach (PdfReader pdfReader in this.documents)
			{
				for (int i = 1; i <= pdfReader.NumberOfPages; i++)
				{
					document.NewPage();
					directContent.AddTemplate(instance.GetImportedPage(pdfReader, i), 0f, 0f);
					if (this.EnablePagination)
					{
						directContent.BeginText();
						directContent.SetFontAndSize(this.baseFont, 9f);
						int num1 = num;
						num = num1 + 1;
						directContent.ShowTextAligned(1, string.Format("{0} de {1}", num1, this.totalPages), 520f, 5f, 0f);
						directContent.EndText();
					}
				}
			}
		}
		finally
		{
			outputStream.Flush();
			if (document != null)
			{
				document.Close();
			}
			outputStream.Close();
		}
	}

	public void MergeLandScape(Stream outputStream)
	{
		if (outputStream == null || !outputStream.CanWrite)
		{
			throw new Exception("OutputStream is not as per prescribed format");
		}
		Document document = null;
		try
		{
			document = new Document(PageSize.A4.Rotate());
			PdfWriter instance = PdfWriter.GetInstance(document, outputStream);
			document.Open();
			PdfContentByte directContent = instance.DirectContent;
			if (this.EnablePagination)
			{
				this.documents.ForEach((PdfReader doc) => {
					PdfMerge numberOfPages = this;
					numberOfPages.totalPages = numberOfPages.totalPages + doc.NumberOfPages;
				});
			}
			int num = 1;
			foreach (PdfReader pdfReader in this.documents)
			{
				for (int i = 1; i <= pdfReader.NumberOfPages; i++)
				{
					document.NewPage();
					directContent.AddTemplate(instance.GetImportedPage(pdfReader, i), 0f, 0f);
					if (this.EnablePagination)
					{
						directContent.BeginText();
						directContent.SetFontAndSize(this.baseFont, 9f);
						int num1 = num;
						num = num1 + 1;
						directContent.ShowTextAligned(1, string.Format("{0} de {1}", num1, this.totalPages), 520f, 5f, 0f);
						directContent.EndText();
					}
				}
			}
		}
		finally
		{
			outputStream.Flush();
			if (document != null)
			{
				document.Close();
			}
			outputStream.Close();
		}
	}

	public void MergePortrait(Stream outputStream)
	{
		if (outputStream == null || !outputStream.CanWrite)
		{
			throw new Exception("OutputStream is not as per prescribed format");
		}
		Document document = null;
		try
		{
			document = new Document(PageSize.A4);
			PdfWriter instance = PdfWriter.GetInstance(document, outputStream);
			document.Open();
			PdfContentByte directContent = instance.DirectContent;
			if (this.EnablePagination)
			{
				this.documents.ForEach((PdfReader doc) => {
					PdfMerge numberOfPages = this;
					numberOfPages.totalPages = numberOfPages.totalPages + doc.NumberOfPages;
				});
			}
			int num = 1;
			foreach (PdfReader pdfReader in this.documents)
			{
				for (int i = 1; i <= pdfReader.NumberOfPages; i++)
				{
					document.NewPage();
					directContent.AddTemplate(instance.GetImportedPage(pdfReader, i), 0f, 0f);
					if (this.EnablePagination)
					{
						directContent.BeginText();
						directContent.SetFontAndSize(this.baseFont, 9f);
						int num1 = num;
						num = num1 + 1;
						directContent.ShowTextAligned(1, string.Format("{0} de {1}", num1, this.totalPages), 520f, 5f, 0f);
						directContent.EndText();
					}
				}
			}
		}
		finally
		{
			outputStream.Flush();
			if (document != null)
			{
				document.Close();
			}
			outputStream.Close();
		}
	}
}