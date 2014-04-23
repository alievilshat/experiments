<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview/str_deliveryservice">
          <product>
            <productid m:left="-98" m:top="149">pk:product(delivery-<xsl:value-of select="id" />)</productid>
            <deliverymaximumordersum m:left="-25" m:top="550">
              <xsl:value-of select="maxsalesprice" />
            </deliverymaximumordersum>
            <deliveryservicemaximumweight m:left="-149" m:top="582">
              <xsl:value-of select="maxweight" />
            </deliveryservicemaximumweight>
            <producttypeid m:left="33" m:top="191">1</producttypeid>
            <name m:left="-25" m:top="481">
              <xsl:value-of select="servicename" />
            </name>
          </product>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>