<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select * from str_documenttemplatedetails')">
          <templatedetails>
            <templatedetailsid m:left="40" m:top="158">pk:templatedetails(document-<xsl:value-of select="id" />)</templatedetailsid>
            <xsl:if test="businessid != 0">
              <businessid m:left="-92" m:top="176">
                <xsl:value-of select="businessid" />
              </businessid>
            </xsl:if>
            <templateid m:left="40" m:top="191">fk:template(document-<xsl:value-of select="templateid" />)</templateid>
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
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>