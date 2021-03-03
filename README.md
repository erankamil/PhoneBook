# PhoneBook

A class representing a phone book.
I decieded to use an XML file to save the phone book data since C# provides LINQ to XML integration.
This integration enables you to write queries on the in-memory XML document to retrieve collections of elements and attributes.
I used the functional construction approach that uses tXElement and XAttribute object constructors creating XML trees and easily transform XML trees from one shape to another.

 * I chose the XML format although there in newer file formats etc Json that has thinner structure and takes up less memory space, Because I wanted to use  and experience with .NET tools.
 * The XML file will located with the exexutable file that uses it.

