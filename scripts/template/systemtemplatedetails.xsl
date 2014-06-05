<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query(user:query())">
          <templatedetails>
            <templatedetailsid m:left="43" m:top="127">pk:templatedetails(document-<xsl:value-of select="id" />)</templatedetailsid>
            <xsl:if test="businessid != 0">
              <businessid m:left="-87" m:top="152">
                <xsl:value-of select="businessid" />
              </businessid>
            </xsl:if>
            <templateid m:left="37" m:top="180">fk:template(system - <xsl:value-of select="templatename" />)</templateid>
            <templatesubject m:left="-94" m:top="209">
              <xsl:value-of select="templatesubject" />
            </templatesubject>
            <templatebody m:left="29" m:top="226">
              <xsl:value-of select="templatebody" />
            </templatebody>
            <languageid m:left="-94" m:top="243">
              <xsl:value-of select="languageid" />
            </languageid>
            <senderemail m:left="31" m:top="260"></senderemail>
          </templatedetails>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
  <msxsl:script implements-prefix="user" language="CSharp"><![CDATA[
    public string query()
    {
        return "select t.templatename, td.* from str_documenttemplatedetails td inner join str_documenttemplate t on t.id = td.templateid and t.systemtype = 1";
    }
    ]]></msxsl:script>
</xsl:stylesheet>