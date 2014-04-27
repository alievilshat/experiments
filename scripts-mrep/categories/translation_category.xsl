<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select wi.id, w.businessid, wi.languageid, wi.categoryid, wi.name from cms_websiteitems wi inner join cms_website w on w.websiteid = wi.websiteid where wi.name is not null and categoryid is not null and w.businessid = 1')">
          <translation>
            <translationid m:left="15" m:top="200">pk:translation(category-categoryname-<xsl:value-of select="categoryid" />-<xsl:value-of select="languageid" />)</translationid>
            <rowid m:left="8" m:top="270">
              <xsl:value-of select="categoryid" />
            </rowid>
            <languageid m:left="5" m:top="338">
              <xsl:value-of select="languageid" />
            </languageid>
            <tablename>category</tablename>
            <fieldname>categoryname</fieldname>
            <translation m:left="22" m:top="108">
              <xsl:value-of select="name" />
            </translation>
          </translation>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>