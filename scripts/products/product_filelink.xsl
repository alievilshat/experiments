<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query(&quot;select * from v_files where object ilike 'Product' and objectid in (select p.id from str_product p inner join str_category c on c.id = p.categoryid where not p.deleted and (p.parentid is null or p.parentid in (select id from str_product where not deleted)))&quot;)">
          <filelink>
            <filelinkid m:left="-123" m:top="161">pk:filelink(<xsl:value-of select="id" />)</filelinkid>
            <filetypeid m:left="1" m:top="180">4</filetypeid>
            <referenceid m:left="-126" m:top="196">
              <xsl:value-of select="objectid" />
            </referenceid>
            <fileid m:left="2" m:top="213">
              <xsl:value-of select="id" />
            </fileid>
          </filelink>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>