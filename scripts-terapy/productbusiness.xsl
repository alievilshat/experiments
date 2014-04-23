<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:terapy="http://www.navitas.nl/2014/Mapper/dbaccess/terapy" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <terapy>
        <xsl:for-each select="terapy:table('_products')">
          <productbusiness>
            <productbusinessid m:left="-64" m:top="161">pk:productbusiness(<xsl:value-of select="productnumber" />)</productbusinessid>
            <productid m:left="57" m:top="179">fk:product(<xsl:value-of select="productnumber" />)</productid>
            <salesprice m:left="-66" m:top="195">
              <xsl:value-of select="price" />
            </salesprice>
            <iswebsite m:left="67" m:top="438">1</iswebsite>
            <businessid m:left="68" m:top="470">1</businessid>
          </productbusiness>
        </xsl:for-each>
      </terapy>
    </target>
  </xsl:template>
</xsl:stylesheet>