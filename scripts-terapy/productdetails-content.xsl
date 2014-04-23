<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:terapy="http://www.navitas.nl/2014/Mapper/dbaccess/terapy" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <terapy>
        <xsl:for-each select="terapy:table('_products')">
          <productdetails>
            <productdetailsid m:left="-5" m:top="167">pk:productdetails(color-<xsl:value-of select="productnumber" />)</productdetailsid>
            <productid m:left="-126" m:top="183">fk:product(<xsl:value-of select="productnumber" />)</productid>
            <featureid m:left="-5" m:top="199">1</featureid>
            <featurevalue m:left="-125" m:top="216">
              <xsl:value-of select="colour" />
            </featurevalue>
          </productdetails>
        </xsl:for-each>
      </terapy>
    </target>
  </xsl:template>
</xsl:stylesheet>