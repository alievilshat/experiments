<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select ds.*, dt.type as deliverytype, sb.stockid from str_deliveryservice ds inner join str_deliverytype dt on dt.id = ds.deliverytypeid inner join str_stockbusiness sb on sb.businessid = ds.businessid')">
          <product>
            <productid m:left="-90" m:top="178">pk:product(delivery-<xsl:value-of select="id" />-<xsl:value-of select="stockid" />-1)</productid>
            <deliverymaximumordersum m:left="12" m:top="626">
              <xsl:value-of select="maxsalesprice" />
            </deliverymaximumordersum>
            <deliveryservicemaximumweight m:left="-111" m:top="642">
              <xsl:value-of select="maxweight" />
            </deliveryservicemaximumweight>
            <producttypeid m:left="46" m:top="248">1</producttypeid>
            <name m:left="23" m:top="528">
              <xsl:value-of select="deliverytype" />
            </name>
            <servicecountryid m:left="27" m:top="462">1</servicecountryid>
            <stockdeliverycompanyid m:left="-100" m:top="478">fk:stockdeliverycompany(<xsl:value-of select="deliverycompanyid" />-<xsl:value-of select="businessid" />-<xsl:value-of select="stockid" />)</stockdeliverycompanyid>
            <taxcategoryid m:left="-98" m:top="446">2</taxcategoryid>
          </product>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>