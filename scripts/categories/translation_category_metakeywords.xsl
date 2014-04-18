<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select wi.id, w.businessid, wi.languageid, wi.categoryid, wi.metakeywords from cms_websiteitems wi inner join cms_website w on w.websiteid = wi.websiteid where wi.metakeywords is not null and categoryid is not null and w.businessid = 3')">
          <translation>
            <translationid m:left="-191" m:top="179">pk:translation(category-metakeywords-<xsl:value-of select="categoryid" />-<xsl:value-of select="languageid" />)</translationid>
            <rowid m:left="-17" m:top="259">
              <xsl:value-of select="categoryid" />
            </rowid>
            <languageid m:left="6" m:top="338">
              <xsl:value-of select="languageid" />
            </languageid>
            <tablename>category</tablename>
            <fieldname>metakeywords</fieldname>
            <translation m:left="22" m:top="108">
              <xsl:value-of select="metakeywords" />
            </translation>
          </translation>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>