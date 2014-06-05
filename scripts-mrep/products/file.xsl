<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select id, imageheight, imagewidth, languageid, fileobjecttypeid, filename, filetype, dateuploaded, dateedited, employeeuploadedid, employeeeditedid, filesize, published, folderid from str_fileobjects where fileobjecttypeid &lt; 100 and id &gt; 219000')">
          <file>
            <fileid m:left="-129" m:top="162">
              <xsl:value-of select="id" />
            </fileid>
            <imageheight m:left="-5" m:top="177">
              <xsl:value-of select="imageheight" />
            </imageheight>
            <imagewidth m:left="-133" m:top="193">
              <xsl:value-of select="imagewidth" />
            </imagewidth>
            <languageid m:left="-4" m:top="211">
              <xsl:value-of select="languageid" />
            </languageid>
            <filetypeid m:left="-130" m:top="229">
              <xsl:value-of select="fileobjecttypeid" />
            </filetypeid>
            <filename m:left="-5" m:top="248">
              <xsl:value-of select="filename" />
            </filename>
            <extention m:left="-130" m:top="268">
              <xsl:value-of select="filetype" />
            </extention>
            <dateuploaded m:left="-3" m:top="285">
              <xsl:value-of select="dateuploaded" />
            </dateuploaded>
            <dateedited m:left="-125" m:top="306">
              <xsl:value-of select="dateedited" />
            </dateedited>
            <employeeuploadedid m:left="-2" m:top="324">
              <xsl:value-of select="employeeuploadedid" />
            </employeeuploadedid>
            <employeeeditedid m:left="-129" m:top="341">
              <xsl:value-of select="employeeeditedid" />
            </employeeeditedid>
            <filesize m:left="-2" m:top="360">
              <xsl:value-of select="filesize" />
            </filesize>
            <file m:left="-131" m:top="377">http://www.moderepubliek.nl/_images/original/<xsl:value-of select="id" />.<xsl:value-of select="filetype" /></file>
            <published m:left="-129" m:top="411">
              <xsl:value-of select="published" />
            </published>
            <folderid m:left="-1" m:top="391">
              <xsl:value-of select="folderid" />
            </folderid>
          </file>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
  <msxsl:script implements-prefix="user" language="CSharp"><![CDATA[
    public string formantContentType(string key)
    {  return key.Substring(1);
    }
    public string formantFilename(string name)
    {  return System.IO.Path.GetFileNameWithoutExtension(name);
    }
]]></msxsl:script>
</xsl:stylesheet>