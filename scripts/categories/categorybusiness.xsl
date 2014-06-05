<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select distinct p.categoryid, ps.subsidiaryid, (case when wi is null then 0 else 1 end) as iswebsite from str_product p inner join str_productsubsidiary ps on ps.productid = p.id  inner join cms_website w on w.businessid = ps.subsidiaryid left join cms_websiteitems wi on wi.categoryid = p.categoryid and wi.websiteid = w.websiteid where not deleted and producttype = 0 and p.categoryid != 0 and (parentid is null or parentid in (select id from str_product where not deleted and producttype = 0 and categoryid != 0))')">
          <categorybusiness>
            <categoryid m:left="-76" m:top="268">
              <xsl:value-of select="categoryid" />
            </categoryid>
            <iswebsite m:left="-74" m:top="315">
              <xsl:value-of select="iswebsite" />
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