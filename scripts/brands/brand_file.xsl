<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select b.id, w.imageid, f.filename, f.contenttype from str_brand b inner join cms_websiteitems w on w.brandid = b.id  inner join str_files f on f.id = w.imageid where imageid is not null  and w.languageid = 1 and w.websiteid = 6')">
          <file>
            <fileid m:left="-270" m:top="68"><xsl:value-of select="imageid" /></fileid>
            <filetypeid m:left="-18" m:top="160">1</filetypeid>
            <filename m:left="-214" m:top="209">
              <xsl:value-of select="filename" />
            </filename>
            <extention m:left="-132" m:top="280">
              <xsl:value-of select="user:formantContentType(contenttype)" />
            </extention>
            <languageid m:left="-15" m:top="118">1</languageid>
            <file m:left="-212" m:top="11">http://www.bodylab.nl/CMSImages/<xsl:value-of select="filename" />|http://www.worldofchemicals.com/Woclite/tmp/chem/no_image.gif</file>
          </file>
        </xsl:for-each>
      </bodyview3>
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