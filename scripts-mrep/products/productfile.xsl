<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select o.id, p.id as productid, 0 as isdefault from str_fileobjects o inner join str_product p on coalesce(p.parentid, p.id) = o.productid and p.variantvalue2 = o.variantvalue2 where p.id in (select p.id from str_product p inner join str_category c on c.id = p.categoryid where not deleted and (p.parentid is null or p.parentid in (select id from str_product where not deleted)))')">
          <productfile>
            <productfileid m:left="-21" m:top="130">pk:productfile(<xsl:value-of select="id" />-<xsl:value-of select="productid" />)</productfileid>
            <productid m:left="-20" m:top="196">
              <xsl:value-of select="productid" />
            </productid>
            <fileid m:left="-20" m:top="230">
              <xsl:value-of select="id" />
            </fileid>
            <isdefault m:left="-20" m:top="262">
              <xsl:value-of select="isdefault" />
            </isdefault>
          </productfile>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>