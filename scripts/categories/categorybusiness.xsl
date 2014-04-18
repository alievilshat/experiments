<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select distinct p.categoryid, ps.subsidiaryid from str_product p inner join str_productsubsidiary ps on ps.productid = p.id  where not deleted and producttype = 0 and categoryid != 0 and (parentid is null or parentid in (select id from str_product where not deleted and producttype = 0 and categoryid != 0))')">
          <categorybusiness>
            <categoryid m:left="-70" m:top="298">
              <xsl:value-of select="categoryid" />
            </categoryid>
            <iswebsite m:left="-76" m:top="296">
              1
            </iswebsite>
            <businessid m:left="-74" m:top="218">
              <xsl:value-of select="subsidiaryid" />
            </businessid>
            <categorybusinessid m:left="-186" m:top="132">pk:categorybusiness(<xsl:value-of select="subsidiaryid" />-<xsl:value-of select="categoryid" />)</categorybusinessid>
          </categorybusiness>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>