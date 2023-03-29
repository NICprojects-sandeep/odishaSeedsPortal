using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

public class PdfWriterEvents : IPdfPageEvent
{
	private string watermarkText = string.Empty;

	public PdfWriterEvents(string watermark)
	{
		this.watermarkText = watermark;
	}

    public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
    {
    }

    public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition)
    {
    }

    public void OnCloseDocument(PdfWriter writer, Document document)
    {
    }

    public void OnEndPage(PdfWriter writer, Document document)
    {
    }

    public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
    {
    }

    public void OnOpenDocument(PdfWriter writer, Document document)
    {
    }

    public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
    {
    }

    public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition)
    {
    }

    public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title)
    {
    }

    public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition)
    {
    }

	public void OnStartPage(PdfWriter writer, Document document)
	{
		float single = 80f;
		float single1 = 300f;
		float single2 = 400f;
		float single3 = 45f;
		try
		{
			PdfContentByte directContentUnder = writer.DirectContentUnder;
			BaseFont baseFont = BaseFont.CreateFont("Helvetica", "Cp1252", true);
			directContentUnder.BeginText();
			directContentUnder.SetColorFill(BaseColor.LIGHT_GRAY);
			directContentUnder.SetFontAndSize(baseFont, single);
			directContentUnder.ShowTextAligned(1, this.watermarkText, single1, single2, single3);
			directContentUnder.EndText();
		}
		catch (Exception exception)
		{
			Console.Error.WriteLine(exception.Message);
		}
	}
}