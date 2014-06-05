<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query(user:query())">
          <file>
            <fileid m:left="-156" m:top="99">
              <xsl:value-of select="id" />
            </fileid>
            <filetypeid m:left="3" m:top="134">1</filetypeid>
            <filename m:left="160" m:top="187">
              <xsl:value-of select="filename" />
            </filename>
            <extention m:left="15" m:top="244">
              <xsl:value-of select="user:formantContentType(contenttype)" />
            </extention>
            <file m:left="-539" m:top="21">http://www.bodylab.nl/CMSImages/<xsl:value-of select="filename" /></file>
          </file>
          <filelink>
            <filelinkid m:left="-563" m:top="230">pk:filelink(category-<xsl:value-of select="brandid" />-<xsl:value-of select="languageid" />-1)</filelinkid>
            <filetypeid m:left="-322" m:top="395">1</filetypeid>
            <referenceid m:left="-532" m:top="494">
              <xsl:value-of select="brandid" />
            </referenceid>
            <fileid m:left="-311" m:top="560">
              <xsl:value-of select="id" />
            </fileid>
            <languageid m:left="-72" m:top="633">
              <xsl:value-of select="languageid" />
            </languageid>
            <sequence m:left="181" m:top="655">1</sequence>
          </filelink>
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
    public string query()
    {
        return @"select v1.id, s.businessid, w.languageid, v1.contenttype, w.brandid, w.imageid as imageid, v1.filename --w.imagemouseoverid as imageid2, w.imageselectedid  as imageid3, 
          from cms_websiteitems w
          inner join cms_website s on s.businessid = w.websiteid
          left join v_files v1 on v1.id = w.imageid
          where w.brandid is not null and w.imageid is not null and w.websiteid = 6";
    }
]]></msxsl:script>
</xsl:stylesheet>