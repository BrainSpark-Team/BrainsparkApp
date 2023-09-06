window.jsPDF = window.jspdf.jsPDF;

// wwwroot/interop.js
window.downloadFile = (filename, data) => {
    const blob = new Blob([data], { type: 'application/pdf' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    window.URL.revokeObjectURL(url);
    document.body.removeChild(a);
};

window.generatePDF = (title, paragraphs, logoUrl) => {
    const doc = new jsPDF(); // Use the global jsPDF object
    const pageWidth = doc.internal.pageSize.width;

    doc.setFont('Montserrat');
    doc.setTextColor(14, 41, 84);

    // Add logo to the left-top corner
    const logoWidth = 20;
    doc.addImage(logoUrl, 'PNG', 5, 9, logoWidth, 16);

    doc.setFontSize(26); // Set font size for the title

    const titleWidth = doc.getTextWidth(title);
    const titleX = (pageWidth - titleWidth) / 2; // Center the title horizontally
    doc.text(title, titleX, 20); // Position the title at Y = 20

    doc.setFontSize(16); // Set font size for paragraphs

    let yPosition = 50; // Start position for paragraphs

    for (const paragraph of paragraphs) {
        doc.text(paragraph, 10, yPosition);
        yPosition += 10; // Increase Y position for the next paragraph
    }

    const pdfData = doc.output('arraybuffer');
    const pdfBase64 = btoa(String.fromCharCode(...new Uint8Array(pdfData)));
    return pdfBase64;
};




