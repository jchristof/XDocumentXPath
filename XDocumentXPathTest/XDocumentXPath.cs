
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XDocumentXPathTest {
    [TestClass]
    public class XDocumentXPath {
        #region Books Xml
        const string xml = @"
                    <catalog>
                       <book id='bk101'>
                          <author>Gambardella, Matthew</author>
                          <title>XML Developer's Guide</title>
                          <genre>Computer</genre>
                          <price>44.95</price>
                          <publish_date>2000-10-01</publish_date>
                          <description>An in-depth look at creating applications 
                          with XML.</description>
                       </book>
                       <book id='bk102'>
                          <author>Ralls, Kim</author>
                          <title>Midnight Rain</title>
                          <genre>Fantasy</genre>
                          <price>5.95</price>
                          <publish_date>2000-12-16</publish_date>
                          <description>A former architect battles corporate zombies, 
                          an evil sorceress, and her own childhood to become queen 
                          of the world.</description>
                       </book>
                       <book id='bk103'>
                          <author>Corets, Eva</author>
                          <title>Maeve Ascendant</title>
                          <genre>Fantasy</genre>
                          <price>5.95</price>
                          <publish_date>2000-11-17</publish_date>
                          <description>After the collapse of a nanotechnology 
                          society in England, the young survivors lay the 
                          foundation for a new society.</description>
                       </book>
                       <book id='bk104'>
                          <author>Corets, Eva</author>
                          <title>Oberon's Legacy</title>
                          <genre>Fantasy</genre>
                          <price>5.95</price>
                          <publish_date>2001-03-10</publish_date>
                          <description>In post-apocalypse England, the mysterious 
                          agent known only as Oberon helps to create a new life 
                          for the inhabitants of London. Sequel to Maeve 
                          Ascendant.</description>
                       </book>
                       <book id='bk105'>
                          <author>Corets, Eva</author>
                          <title>The Sundered Grail</title>
                          <genre>Fantasy</genre>
                          <price>5.95</price>
                          <publish_date>2001-09-10</publish_date>
                          <description>The two daughters of Maeve, half-sisters, 
                          battle one another for control of England. Sequel to 
                          Oberon's Legacy.</description>
                       </book>
                       <book id='bk106'>
                          <author>Randall, Cynthia</author>
                          <title>Lover Birds</title>
                          <genre>Romance</genre>
                          <price>4.95</price>
                          <publish_date>2000-09-02</publish_date>
                          <description>When Carla meets Paul at an ornithology 
                          conference, tempers fly as feathers get ruffled.</description>
                       </book>
                       <book id='bk107'>
                          <author>Thurman, Paula</author>
                          <title>Splish Splash</title>
                          <genre>Romance</genre>
                          <price>4.95</price>
                          <publish_date>2000-11-02</publish_date>
                          <description>A deep sea diver finds true love twenty 
                          thousand leagues beneath the sea.</description>
                       </book>
                       <book id='bk108'>
                          <author>Knorr, Stefan</author>
                          <title>Creepy Crawlies</title>
                          <genre>Horror</genre>
                          <price>4.95</price>
                          <publish_date>2000-12-06</publish_date>
                          <description>An anthology of horror stories about roaches,
                          centipedes, scorpions  and other insects.</description>
                       </book>
                       <book id='bk109'>
                          <author>Kress, Peter</author>
                          <title>Paradox Lost</title>
                          <genre>Science Fiction</genre>
                          <price>6.95</price>
                          <publish_date>2000-11-02</publish_date>
                          <description>After an inadvertant trip through a Heisenberg
                          Uncertainty Device, James Salway discovers the problems 
                          of being quantum.</description>
                       </book>
                       <book id='bk110'>
                          <author>O'Brien, Tim</author>
                          <title>Microsoft .NET: The Programming Bible</title>
                          <genre>Computer</genre>
                          <price>36.95</price>
                          <publish_date>2000-12-09</publish_date>
                          <description>Microsoft's .NET initiative is explored in 
                          detail in this deep programmer's reference.</description>
                       </book>
                       <book id='bk111'>
                          <author>O'Brien, Tim</author>
                          <title>MSXML3: A Comprehensive Guide</title>
                          <genre>Computer</genre>
                          <price>36.95</price>
                          <publish_date>2000-12-01</publish_date>
                          <description>The Microsoft MSXML3 parser is covered in 
                          detail, with attention to XML DOM interfaces, XSLT processing, 
                          SAX and more.</description>
                       </book>
                       <book id='bk112'>
                          <author>Galos, Mike</author>
                          <title>Visual Studio 7: A Comprehensive Guide</title>
                          <genre>Computer</genre>
                          <price>49.95</price>
                          <publish_date>2001-04-16</publish_date>
                          <description>Microsoft Visual Studio 7 is explored in depth,
                          looking at how Visual Basic, Visual C++, C#, and ASP+ are 
                          integrated into a comprehensive development 
                          environment.</description>
                       </book>
                    </catalog>";
        #endregion

        private XDocument XDocument => XDocument.Parse(xml);

        /// <summary>
        /// Transform the return of XPathEvaluate() into a List of XObject
        /// </summary>
        /// <param name="results">The object returned from XPathEvaluate()</param>
        /// <returns>List of XObject</returns>
        private List<XObject> AsList(object results) {
            if (results is IEnumerable enumerable) {
                if (enumerable.Cast<XObject>() is IEnumerable<XObject> elements) {
                    return elements.ToList();
                }
            }

            Assert.Fail();
            return new List<XObject>();
        }

        /// <summary>
        /// Transform the return of XPathEvaluate() into a string
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private string AsString(object results) {
            if (results is IEnumerable enumerable) {
                if (enumerable.Cast<XObject>() is IEnumerable<XObject> elements) {
                    return string.Concat(elements.Select(element => element.ToString()));
                }
            }

            Assert.Fail();
            return results.ToString();
        }

        [TestMethod]
        public void XDocumentXPath_FindAllBooks() {
            var list = AsList(XDocument.XPathEvaluate("//book"));
            Assert.IsTrue(list.Count == 12);
        }

        [TestMethod]
        public void XDocumentXPath_FindFirstBookTitle() {
            var value = XDocument.XPathEvaluate("/catalog/book[1]/title/text()");

            var stringResult = AsString(value);
            var listResult = AsList(value);

            Assert.IsTrue(stringResult == "XML Developer's Guide");
            Assert.IsTrue(listResult.Count == 1);
        }

        [TestMethod]
        public void XDocumentXPath_FindSecondToLastBookAuthor() {
            var value = XDocument.XPathEvaluate("/catalog/book[last()-1]/author/text()");

            var stringResult = AsString(value);
            var listResult = AsList(value);

            Assert.IsTrue(stringResult == "O'Brien, Tim");
            Assert.IsTrue(listResult.Count == 1);
        }

        [TestMethod]
        public void XDocumentXPath_FindBooksByPosition() {
            var value = XDocument.XPathEvaluate("/catalog/book[position()<3]");

            var listResult = AsList(value);
            Assert.IsTrue(listResult.Count == 2);

            var xDocument = XDocument.Parse(listResult[0].ToString());

            var firstTitle = AsString(xDocument.XPathEvaluate("book/title/text()"));
            Assert.IsTrue(firstTitle == "XML Developer's Guide");

            firstTitle = AsString(xDocument.XPathEvaluate("//title/text()"));
            Assert.IsTrue(firstTitle == "XML Developer's Guide"); 
        }
    }
}
