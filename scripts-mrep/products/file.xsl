<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query(&quot;select * from v_files where object ilike 'Product' and objectid in (select id from str_product where not deleted and true and (parentid is null or parentid in (select id from str_product where not deleted and true)))&quot;)">
          <file>
            <fileid m:left="-51" m:top="161">
              <xsl:value-of select="id" />
            </fileid>
            <filetypeid m:left="-99" m:top="334">4</filetypeid>
            <filename m:left="-118" m:top="230">
              <xsl:value-of select="filename" />
            </filename>
            <extention m:left="28" m:top="262">
              <xsl:value-of select="user:formantContentType(contenttype)" />
            </extention>
            <file m:left="-51" m:top="351">http://www.bodylab.nl/_images/original/<xsl:value-of select="id" /><xsl:value-of select="contenttype" />|http://www.worldofchemicals.com/Woclite/tmp/chem/no_image.gif</file>
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