<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select wi.id, w.businessid, wi.languageid, wi.categoryid, wi.title from cms_websiteitems wi inner join cms_website w on w.websiteid = wi.websiteid where wi.title is not null and categoryid is not null and w.businessid = 1')">
          <translation>
            <translationid m:left="-218" m:top="192">pk:translation(category-title-<xsl:value-of select="categoryid" />-<xsl:value-of select="languageid" />)</translationid>
            <rowid m:left="-18" m:top="258">
              <xsl:value-of select="categoryid" />
            </rowid>
            <languageid m:left="6" m:top="338">
              <xsl:value-of select="languageid" />
            </languageid>
            <tablename>category</tablename>
            <fieldname>title</fieldname>
            <translation m:left="22" m:top="108">
              <xsl:value-of select="title" />
            </translation>
          </translation>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>