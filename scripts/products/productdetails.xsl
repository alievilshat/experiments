<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_productdetails where productid in (select p.id from str_product p inner join str_category c on c.id = p.categoryid where not p.deleted and (p.parentid is null or p.parentid in (select id from str_product where not deleted)))')">
          <productdetails>
            <productdetailsid m:left="-20" m:top="168" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </productdetailsid>
            <productid m:left="-144" m:top="184" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="productid" />
            </productid>
            <featureid m:left="-20" m:top="200" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="featureid" />
            </featureid>
            <featurevalue m:left="-144" m:top="218" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="featurevalue" />
            </featurevalue>
            <featurememo m:left="-20" m:top="266" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="featurememo" />
            </featurememo>
          </productdetails>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>