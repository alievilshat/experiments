<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_productpackage where productid in (select id from str_product where not deleted and categoryid != 0) and parentproductid in (select id from str_product where not deleted and categoryid != 0)')">
          <productpackage>
            <parentproductid m:left="-54" m:top="128">
              <xsl:value-of select="parentproductid" />
            </parentproductid>
            <productid m:left="-53" m:top="165">
              <xsl:value-of select="productid" />
            </productid>
            <quantity m:left="-51" m:top="196">
              <xsl:value-of select="quantity" />
            </quantity>
            <sequenceid m:left="-52" m:top="226">
              <xsl:value-of select="sequenceid" />
            </sequenceid>
            <isdefault m:left="-51" m:top="257">
              <xsl:value-of select="isdefault" />
            </isdefault>
            <packageproductid m:left="-54" m:top="288">pk:productpackage(<xsl:value-of select="parentproductid" />-<xsl:value-of select="productid" />)</packageproductid>
          </productpackage>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>